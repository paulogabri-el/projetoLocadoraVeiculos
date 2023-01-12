using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using ProjetoLocadoraDeVeiculos.Helper;

namespace ProjetoLocadoraDeVeiculos.Models.ViewModels
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
