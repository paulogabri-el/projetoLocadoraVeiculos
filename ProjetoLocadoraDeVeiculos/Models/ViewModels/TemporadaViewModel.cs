using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjetoLocadoraDeVeiculos.Models.ViewModels
{
    public class TemporadaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome da temporada é obrigatório.")]
        [DisplayName("Nome temporada")]
        [RegularExpression(@"^[a-zA-Zà-úÀ-Ú ]+$", ErrorMessage = "Você inseriu caracteres inválidos para o nome.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O percentual a acrescer na diária é obrigatório.")]
        [DisplayName("Percentual a acrescer diária")]
        public string PercentualAcrescerDiaria { get; set; }

        [Required(ErrorMessage = "O percentual a acrescer na multa diária é obrigatório.")]
        [DisplayName("Percentual a acrescer multa diária")]
        public string PercentualAcrescerMultaFixa { get; set; }

        [Required(ErrorMessage = "O percentual a acrescer na multa fixa é obrigatório.")]
        [DisplayName("Percentual a acrescer multa fixa")]
        public string PercentualAcrescerMultaDiaria { get; set; }
    }
}
