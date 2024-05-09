using System.Drawing;
using TechChallenge_Fase1.Model;

namespace TechChallenge_Fase1.Interfaces
{
    public interface ICarteiraRepository : IComumRepository<Carteira>
    {
        Carteira GetCarteiraByUsuarioID(int id);
        void AdicionarValorCarteira(int id, float valor);
        bool RemoverValorCarteira(int id, float valor);
        bool ComprarAcoes(int IdUsuario, int IdAcao, int quantidade, bool enviaServiceBus = false);
        bool VenderAcoes(int IdUsuario, int IdAcao, int quantidade);
    }
}
