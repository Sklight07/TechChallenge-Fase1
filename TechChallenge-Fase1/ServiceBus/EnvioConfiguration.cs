using MassTransit;
using TechChallenge_Fase1.Interfaces;
using TechChallenge_Fase1.Model;
using TechChallenge_Fase1.Requests.Carteira;

namespace TechChallenge_Fase1.ServiceBus
{
    public class EnvioConfiguration : IServiceBusRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IBus _bus;

        public EnvioConfiguration(IBus bus, IConfiguration configuration)
        {
            _bus = bus;
            _configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task ComprarAcoes(ComprarAcoesServiceBus message)
        {
            try
            {
                var nomeFila = _configuration.GetSection("MassTransitAzure")["NomeFila"] ?? string.Empty;
                var endpoint = await _bus.GetSendEndpoint(new Uri($"queue:{nomeFila}"));
                await endpoint.Send(message);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task EnviarMensagem(CompraAcoesRequest message)
        {
            var nomeFila = _configuration.GetSection("MassTransitAzure")["NomeFila"] ?? string.Empty;
            var endpoint = await _bus.GetSendEndpoint(new Uri($"queue:{nomeFila}"));
            await endpoint.Send(message);

        }

    }
}
