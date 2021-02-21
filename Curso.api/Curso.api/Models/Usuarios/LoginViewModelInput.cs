using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Curso.api.Models.Usuarios
{
    public class LoginViewModelInput
    {
        [Required(ErrorMessage = "O Login é Obrigatório")]
        public string Login { get; set; }

        [Required(ErrorMessage = "A senha é Obrigatória")]
        public string Senha { get; set; }
    }
}
