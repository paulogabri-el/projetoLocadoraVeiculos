using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ProjetoLocadoraDeVeiculos.Models.ViewModels
{
    public class ClienteViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Cnh { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
