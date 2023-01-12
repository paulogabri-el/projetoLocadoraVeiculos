using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ProjetoLocadoraDeVeiculos.Models.ViewModels
{
    public class LocacaoViewModel
    {
        public int Id { get; set; }

        public int ClienteId { get; set; }

        public int VeiculoId { get; set; }

        public int StatusLocacaoId { get; set; }

        public int TemporadaId { get; set; }

        public DateTime DataLocacao { get; set; }

        public DateTime DataEntrega { get; set; }

        public int? QtdDiasAlugados { get; set; }

        public decimal ValorDiaria { get; set; } //Calculado com base na diária do carro e adicionado o % em cima com base na temporada escolhida
    }
}
