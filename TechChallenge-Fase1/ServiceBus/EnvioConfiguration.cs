using MassTransit;
using TechChallenge_Fase1.Interfaces;
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

        public async Task EnviarMensagem(CompraAcoesRequest message)
        {
            var nomeFila = _configuration.GetSection("MassTransitAzure")["NomeFila"] ?? string.Empty;
            var endpoint = await _bus.GetSendEndpoint(new Uri($"queue:{nomeFila}"));
            await endpoint.Send(message);

        }

    }
}
