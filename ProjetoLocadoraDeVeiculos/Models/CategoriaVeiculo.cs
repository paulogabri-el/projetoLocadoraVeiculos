using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using NuGet.Packaging.Signing;


namespace ProjetoLocadoraDeVeiculos.Models
{
    public class CategoriaVeiculo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        [RegularExpression(@"^[a-zA-Zà-úÀ-Ú ]+$", ErrorMessage = "Você inseriu caracteres inválidos para o nome.")]
        [DisplayName("Nome da categoria")]
        public string Nome { get; set; }

        [DisplayName("Data de cadastro")]
        [DataType(DataType.Date)]
        public DateTime DataCadastro { get; set; }

        [DisplayName("Data de alteração")]
        [DataType(DataType.Date)]
        public DateTime? DataAlteracao { get; set; }
        public ICollection<Veiculo> Veiculos { get; set; } = new List<Veiculo>();


    }
}
