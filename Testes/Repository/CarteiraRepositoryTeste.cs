using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge_Fase1.Interfaces;
using TechChallenge_Fase1.Model;
using TechChallenge_Fase1.Repository;

namespace Testes.Repository
{
    public class CarteiraRepositoryTeste
    {

        [Theory]
        [InlineData(1,100)]
        [InlineData(-1,200)]
        public void AdicionarValorCarteiraTeste(int id, float valor)
        {
            var teste = new Mock<ICarteiraRepository>().Setup(service => service.AdicionarValorCarteira(id, valor));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(-1)]
        [InlineData(0)]
        public void GetCarteiraByUsuarioIDTeste(int id)
        {
            var teste = new Mock<ICarteiraRepository>().Setup(service => service.GetCarteiraByUsuarioID(id));
        }

        [Theory]
        [InlineData(1, 150)]
        [InlineData(2, 1500)]
        [InlineData(3, 9999)]
        public void RemoverSaldoTeste(int carteiraId, float saldoRemover)
        {
            var teste = new Mock<ICarteiraRepository>().Setup(service => service.RemoverValorCarteira(carteiraId, saldoRemover));
        }


        [Theory]
        [InlineData(1, 1, 150)]
        [InlineData(2, 1, 1500)]
        [InlineData(3, 3, 9999)]
        public void ComprarAcoesTeste(int IdUsuario, int IdAcao, int quantidade)
        {
            var teste = new Mock<ICarteiraRepository>().Setup(service => service.ComprarAcoes(IdUsuario, IdAcao, quantidade, false));
        }

        [Theory]
        [InlineData(1, 1, 150)]
        [InlineData(2, 1, 1500)]
        [InlineData(3, 3, 9999)]
        public void VenderAcoesTeste(int IdUsuario, int IdAcao, int quantidade)
        {
            var teste = new Mock<ICarteiraRepository>().Setup(service => service.VenderAcoes(IdUsuario, IdAcao, quantidade));
        }
    }
}
