using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjetoLocadoraDeVeiculos.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Digite o e-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite a senha")]
        public string Senha { get; set; }

    }
}
