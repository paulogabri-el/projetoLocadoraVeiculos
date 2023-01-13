using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoLocadoraDeVeiculos.Models
{
    public class Locacao
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]

        [DisplayName("Cliente")]
        [ForeignKey("Cliente")]
        public int ClienteId { get; set; }

        [DisplayName("Cliente")]
        public Cliente? Cliente { get; set; }

        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        [DisplayName("Veículo")]
        [ForeignKey("Veiculo")]
        public int VeiculoId { get; set; }
        public Veiculo? Veiculo { get; set; }

        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        [DisplayName("Status locação")]
        [ForeignKey("Status")]
        public int StatusLocacaoId { get; set; }
        [DisplayName("Status locação")]
        public StatusLocacao? StatusLocacao { get; set; }

        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        [DisplayName("Temporada")]
        [ForeignKey("Temporada")]
        public int TemporadaId { get; set; }
        [DisplayName("Temporada")]
        public Temporada? Temporada { get; set; }

        [Required(ErrorMessage = "A data de locação é obrigatória.")]
        [DisplayName("Data locação")]
        [DataType(DataType.Date)]
        public DateTime DataLocacao { get; set; }

        [Required(ErrorMessage = "A data de entrega é obrigatória.")]
        [DisplayName("Data entrega")]
        [DataType(DataType.Date)]
        public DateTime DataEntrega { get; set; }


        [DisplayName("Dias alugados")]
        public int? QtdDiasAlugados { get; set; }

        [DisplayName("Renovações")]
        public int? QtdRenovacoes { get; set; }

        [Required(ErrorMessage = "O valor da diária não pode ser menor ou igual a zero.")]
        [DisplayName("Valor calculado da diária")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public decimal ValorDiaria { get; set; } //Calculado com base na diária do carro e adicionado o % em cima com base na temporada escolhida

        public decimal? ValorTotal { get; set; }

        [DisplayName("Data de cadastro")]
        [DataType(DataType.Date)]
        public DateTime? DataCadastro { get; set; }

        [DisplayName("Data de alteração")]
        [DataType(DataType.Date)]
        public DateTime? DataAlteracao { get; set; }

    }
}
