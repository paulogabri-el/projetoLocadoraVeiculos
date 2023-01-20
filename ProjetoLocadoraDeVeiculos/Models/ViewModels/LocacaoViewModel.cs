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

        public decimal? ValorDiaria { get; set; }

        public decimal? ValorMultaDiaria { get; set; }

        public decimal? ValorMultaFixa { get; set; }

        public decimal? Desconto { get; set; }
    }
}
