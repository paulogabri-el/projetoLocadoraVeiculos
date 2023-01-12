using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjetoLocadoraDeVeiculos.Models
{
    public class Temporada
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        [DisplayName("Nome")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        [DisplayName("Percentual acrescer diária")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public decimal PercentualAcrescerDiaria { get; set; }
        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        [DisplayName("Percentual acrescer multa fixa")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public decimal PercentualAcrescerMultaFixa { get; set; }
        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        [DisplayName("Percentual acrescer multa diária")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public decimal PercentualAcrescerMultaDiaria { get; set; }
        [DisplayName("Data de cadastro")]
        [DataType(DataType.Date)]
        public DateTime? DataCadastro { get; set; }
        [DisplayName("Data de alteração")]
        [DataType(DataType.Date)]
        public DateTime? DataAlteracao { get; set; }
        public ICollection<Locacao> Locacoes { get; set; } = new List<Locacao>(); 

        
    }
}
