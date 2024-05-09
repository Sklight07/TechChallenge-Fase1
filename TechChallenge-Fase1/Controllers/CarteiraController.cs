using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using TechChallenge_Fase1.Enums;
using TechChallenge_Fase1.Interfaces;
using TechChallenge_Fase1.Repository;
using TechChallenge_Fase1.Requests.Carteira;

namespace TechChallenge_Fase1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarteiraController : ControllerBase
    {

        private readonly ILogger<CarteiraController> _logger;
        private readonly ICarteiraRepository _carteiraRepository;
        private readonly IServiceBusRepository _serviceBusRepository;

        public CarteiraController(ILogger<CarteiraController> logger, ICarteiraRepository carteiraRepository, IServiceBusRepository serviceBusRepository)
        {
            _logger = logger;
            _carteiraRepository = carteiraRepository;
            _serviceBusRepository = serviceBusRepository;
        }

        [Authorize]
        [Authorize(Roles = $"{Permissao.Administrador}, {Permissao.UsuarioComum}")]
        [HttpGet]
        [Route(nameof(GetCarteiraByUsuarioId))]
        public IActionResult GetCarteiraByUsuarioId(int id)
        {
            try
            {
                _logger.LogInformation($"Buscado carteira pelo id do usuário: {id}");
                var carteira = _carteiraRepository.GetCarteiraByUsuarioID(id);
                return Ok(carteira);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception: {ex.Message}");
                return BadRequest("Houve um erro durante a busca");
            }
        }

        [Authorize]
        [Authorize(Roles = $"{Permissao.Administrador}, {Permissao.UsuarioComum}")]
        [HttpPost]
        [Route(nameof(AdicionarValorCarteira))]
        public IActionResult AdicionarValorCarteira(int id, float valor)
        {
            try
            {
                _logger.LogInformation($"Adicionando valor {valor} na carteira do usuário {id}");
                _carteiraRepository.AdicionarValorCarteira(id, valor);
                return Ok("Valor adicionado com sucesso");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception: {ex.Message}");
                return BadRequest("Houve um erro durante a adição de valor");
            }
        }

        [Authorize]
        [Authorize(Roles = $"{Permissao.Administrador}, {Permissao.UsuarioComum}")]
        [HttpPost]
        [Route(nameof(RemoverValorCarteira))]
        public IActionResult RemoverValorCarteira(int id, float valor)
        {
            try
            {
                _logger.LogInformation($"Removendo valor {valor} na carteira do usuário {id}");

                if(_carteiraRepository.RemoverValorCarteira(id, valor))
                    return Ok("Valor removido com sucesso");
                else
                    return BadRequest("Não foi possível remover o valor da carteira");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception: {ex.Message}");
                return BadRequest("Houve um erro durante a remoção de valor");
            }
        }

        [Authorize]
        [Authorize(Roles = $"{Permissao.Administrador}, {Permissao.UsuarioComum}")]
        [HttpPost]
        [Route(nameof(ComprarAcao))]
        public IActionResult ComprarAcao([FromBody] CompraAcoesRequest compraAcoesRequest)
        {
            try
            {
                _logger.LogInformation($"Comprando {compraAcoesRequest.Quantidade} ações da: {compraAcoesRequest.IdAcao} para o usuário {compraAcoesRequest.IdUsuario}");
                if (_carteiraRepository.ComprarAcoes(compraAcoesRequest.IdUsuario, compraAcoesRequest.IdAcao, compraAcoesRequest.Quantidade))
                    return Ok("Ação comprada com sucesso");
                else
                    return BadRequest("Não foi possível comprar a ação");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception: {ex.Message}");
                return BadRequest("Houve um erro durante a compra da ação");
            }
        }

        [Authorize]
        [Authorize(Roles = $"{Permissao.Administrador}, {Permissao.UsuarioComum}")]
        [HttpPost]
        [Route(nameof(VenderAcao))]
        public IActionResult VenderAcao([FromBody] CompraAcoesRequest vendaAcoesRequest)
        {
            try
            {
                _logger.LogInformation($"Vendendo {vendaAcoesRequest.Quantidade} ações da: {vendaAcoesRequest.IdAcao} para o usuário {vendaAcoesRequest.IdUsuario}");
                if (_carteiraRepository.VenderAcoes(vendaAcoesRequest.IdUsuario, vendaAcoesRequest.IdAcao, vendaAcoesRequest.Quantidade))
                    return Ok("Ação vendida com sucesso");
                else
                    return BadRequest("Não foi possível vender a ação");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception: {ex.Message}");
                return BadRequest("Houve um erro durante a compra da ação");
            }
        }

        [AllowAnonymous]
        //[Authorize(Roles = $"{Permissao.Administrador}, {Permissao.UsuarioComum}")]
        [HttpPost]
        [Route(nameof(TesteServiceBus))]
        public IActionResult TesteServiceBus([FromBody] CompraAcoesRequest compraAcoesRequest)
        {
            try
            {
                _logger.LogInformation($"Comprando {compraAcoesRequest.Quantidade} ações da: {compraAcoesRequest.IdAcao} para o usuário {compraAcoesRequest.IdUsuario}");

                _carteiraRepository.ComprarAcoes(compraAcoesRequest.IdUsuario, compraAcoesRequest.IdAcao, compraAcoesRequest.Quantidade, true); //_serviceBusRepository.ComprarAcoes(compraAcoesRequest);
                return Ok("enviado com sucesso");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception: {ex.Message}");
                return BadRequest("Houve um erro durante a compra da ação");
            }
        }
    }
}
