using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curso.api.Models.Usuarios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Curso.api.Models.Filters
{
    /// <summary>
    /// filtro usado para evitar code smell na classe usuáriocontroller.cs
    /// ajuda a evitar ctrl+c / ctrl+v de código
    /// </summary>
    public class ValidacaoModelStateCustomizado : ActionFilterAttribute //herda da classe de filtros
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //configurando a resposta para a request em caso de codigo invalido / erro usando link para compor a resposta:
            if (!context.ModelState.IsValid)
            {
                var validaCampoViewModel = new ValidaCampoViewModelOutput(context.ModelState.SelectMany(sm => sm.Value.Errors)
                    .Select(s => s.ErrorMessage));
                //devolve result com a lista de erros
                context.Result = new BadRequestObjectResult(validaCampoViewModel);
            }
        }
    }
}
