using System.ComponentModel.DataAnnotations;

namespace Curso.api.Models.Usuarios
{
    public class RegistroViewModelInput
    {
        [Required(ErrorMessage = "O Login é Obrigatório")]
        public string Login { get; set; }

        [Required(ErrorMessage = "O E-mail é Obrigatório")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A Senha é Obrigatória")]
        public string Senha { get; set; }
    }
}
