using TechChallenge_Fase1.Requests.Carteira;

namespace TechChallenge_Fase1.Interfaces
{
    public interface IServiceBusRepository
    {
        Task EnviarMensagem(CompraAcoesRequest message);
    }
}
