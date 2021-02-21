using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Curso.api.Models.Filters;
using Curso.api.Models.Usuarios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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
            //criando login fake p testar o codigo simulando uma consulta ao banco de dados
            var usuarioViewModelOutput = new UsuarioViewModelOutput()
            {
                Codigo = 1,
                Login = "Fred",
                Email = "fredon.analista@gmail.com"
            };

            //validacao do token do usuario
            var secret = Encoding.ASCII.GetBytes("chavegeradaUsandoSHA256OuOutrodeSuaPreferência*");
            var symetricSecurity = new SymmetricSecurityKey(secret);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuarioViewModelOutput.Codigo.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, usuarioViewModelOutput.Login.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, usuarioViewModelOutput.Email.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(symetricSecurity,SecurityAlgorithms.HmacSha256Signature)
            };
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenGenerated = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(tokenGenerated);

            //autentica o usuario/senha - request de autenticação
            return Ok(new
            {
                Token = token,
                Usuario = loginViewModelInput
            });
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