using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ProjetoLocadoraDeVeiculos.Models.ViewModels
{
    public class LocacaoViewModel
    {

        public int Id { get; set; }

        [DisplayName("Cliente")]
        public int ClienteId { get; set; }

        [DisplayName("Veículo")]
        public int VeiculoId { get; set; }

        [DisplayName("Status")]
        public int StatusLocacaoId { get; set; }
        public StatusLocacao? StatusLocacao { get; set; }

        [DisplayName("Temporada")]
        public int TemporadaId { get; set; }

        [Required(ErrorMessage = "A data de locação é obrigatória!")]
        [DisplayName("Deta de locação")]
        [DataType(DataType.Date)]
        public DateTime DataLocacao { get; set; }

        [Required(ErrorMessage = "A data de entrega é obrigatória!")]
        [DisplayName("Data de entrega")]
        [DataType(DataType.Date)]
        public DateTime DataEntrega { get; set; }

        public int? QtdDiasAlugados { get; set; }

        public decimal? ValorDiaria { get; set; }

        public decimal? ValorMultaDiaria { get; set; }

        public decimal? ValorMultaFixa { get; set; }

        [DisplayName("Desconto")]
        [RegularExpression(@"^[0-9,]+$", ErrorMessage = "Permitido apenas números e virgulas.")]
        public string? Desconto { get; set; }
    }
}
