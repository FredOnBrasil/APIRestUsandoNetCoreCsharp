using Curso.api.Models.Filters;
using Curso.api.Models.Usuarios;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Curso.api.Controllers
{
    /// <summary>
    /// Configuração da Api usando a rota com versionamento
    /// </summary>
    [Route("api/v1/[Controller]")] //rota com versionamento('v1')
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        /// <summary>
        /// Este serviço permite autenticar um usuario cadastradoe ativo.
        /// </summary>
        /// <param name="loginViewModelInput"></param>
        /// <returns>Retorna Status OK, dados do usuario e o Tokenem caso de </returns>
        //aqui usando a biblioteca Swashbuckle.AspNetCore.Annotations do swagger
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao Autenticar", Type = typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Campos Obrigatórios", Type = typeof(ValidaCampoViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro Interno", Type = typeof(ErroGenericoViewModel))]
        [HttpPost]//anotação do estilo estrutural rest
        [Route("logar")]//configurando a primeira rota
        [ValidacaoModelStateCustomizado] //anotacao para usar a configuração de filtro
        public IActionResult Logar(LoginViewModelInput loginViewModelInput)
        {
            //autentica o usuario/senha - request de autenticação
            return Ok(loginViewModelInput);
        }

        [HttpPost]//anotação do estilo estrutural rest
        [Route("registrar")]//configurando a segunda rota
        [ValidacaoModelStateCustomizado] //anotacao para usar a configuração de filtro

        public IActionResult Registrar(RegistroViewModelInput registroViewModelInput)
        {
            return Created("", registroViewModelInput);
        }
    }
}