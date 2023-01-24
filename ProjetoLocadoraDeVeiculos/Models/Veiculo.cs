using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoLocadoraDeVeiculos.Models
{
    [Index(nameof(Placa), IsUnique = true)]
    public class Veiculo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        [DisplayName("Nome")]
        public string Nome { get; set; }

        [DisplayName("Categoria veículo")]
        [ForeignKey("CategoriaVeiculo")]
        public int CategoriaVeiculoId { get; set; }
        [DisplayName("Categoria veículo")]
        public CategoriaVeiculo? CategoriaVeiculo { get; set; }

        
        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        [DisplayName("Placa veículo")]
        [RegularExpression(@"^[a-zA-Z0-9-]+$", ErrorMessage = "É permitido somente letras, números, e hífen.")]
        public string Placa { get; set; }

        [DisplayName("Status veiculo")]
        [ForeignKey("StatusVeiculo")]
        public int StatusVeiculoId { get; set; }
        [DisplayName("Status veiculo")]
        public StatusVeiculo? StatusVeiculo { get; set; }


        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        [DisplayName("Valor diária")]
        [DataType(DataType.Currency)]
        public decimal ValorDiaria { get; set; }

        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        [DisplayName("Valor multa fixa")]
        [DataType(DataType.Currency)]
        public decimal ValorMultaFixa { get; set; }

        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        [DisplayName("Valor multa diária")]
        [DataType(DataType.Currency)]
        public decimal ValorMultaDiaria { get; set; }
        
        [DisplayName("Data de cadastro")]
        [DataType(DataType.Date)]
        public DateTime? DataCadastro { get; set; }
        [DisplayName("Data de alteração")]
        [DataType(DataType.Date)]
        public DateTime? DataAlteracao { get; set; }
        
        public ICollection<Locacao> Locacoes { get; set; } = new List<Locacao>();

    }
}
