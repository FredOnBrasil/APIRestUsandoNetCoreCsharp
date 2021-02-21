using System.Collections.Generic;
using Curso.api.Models.Curso;
using Curso.api.Models.Usuarios;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Curso.api.Controllers
{
    [Route("api/v1/cursos")]
    [ApiController]
    //[Authorize]//protege a API, só aceita requisições se houver autenticação
    public class CursoController : ControllerBase
    {
        /// <summary>
        /// Este serviço Permite cadastrar um curso para um usuário autenticado
        /// </summary>
        /// <returns>Retorna status 201 e dados do curso do usuário</returns>
        [SwaggerResponse(statusCode: 201, description: "Sucesso ao cadastrar um curso")]
        [SwaggerResponse(statusCode: 401, description: "Não Autorizado")]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post(CursoViewModelInput cursoViewModelInput)
        {
            var codigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            return Created("", cursoViewModelInput);
        }

        /// <summary>
        /// Este serviço Permite cadastrar um curso para um usuário autenticado
        /// </summary>
        /// <returns>Retorna status 201 e dados do curso do usuário</returns>
        [SwaggerResponse(statusCode: 201, description: "Sucesso ao cadastrar um curso")]
        [SwaggerResponse(statusCode: 401, description: "Não Autorizado")]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            //devolve uma lista de cursos
            var cursos = new List<CursoViewModelOutput>();
            //preenchendo a lista já que não temos consulta em banco neste caso
            //var codigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            cursos.Add(new CursoViewModelOutput()
            {
                Login = "",
                Descricao = "teste",
                Nome = "teste"
            });

            return Ok();
        }
    }
}