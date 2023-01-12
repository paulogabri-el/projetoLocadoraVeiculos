using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ProjetoLocadoraDeVeiculos.Models.ViewModels
{
    public class VeiculoViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int CategoriaVeiculoId { get; set; }
        public string Placa { get; set; }
        public int StatusVeiculoId { get; set; }
        public decimal ValorDiaria { get; set; }
        public decimal ValorMultaFixa { get; set; }
        public decimal ValorMultaDiaria { get; set; }
    }
}
