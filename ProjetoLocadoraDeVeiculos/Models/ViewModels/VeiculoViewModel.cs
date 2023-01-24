using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ProjetoLocadoraDeVeiculos.Models.ViewModels
{
    public class VeiculoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        [DisplayName("Nome")]
        [RegularExpression(@"^[a-zA-Zà-úÀ-Ú ]+$", ErrorMessage = "Você inseriu caracteres inválidos para o nome.")]
        public string Nome { get; set; }

        [DisplayName("Categoria veículo")]
        public int CategoriaVeiculoId { get; set; }

        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        [DisplayName("Placa veículo")]
        [RegularExpression(@"^[a-zA-Z0-9-]+$", ErrorMessage = "É permitido somente letras, números, e hífen.")]
        public string Placa { get; set; }

        [DisplayName("Status veiculo")]
        public int StatusVeiculoId { get; set; }

        [Required(ErrorMessage = "O valor diária é obrigatório!")]
        [DisplayName("Valor diária")]
        [RegularExpression(@"^[0-9,]+$", ErrorMessage = "Permitido apenas números e virgulas.")]
        public string ValorDiaria { get; set; }

        [Required(ErrorMessage = "O valor da multa fixa é obrigatório!")]
        [DisplayName("Valor multa fixa")]
        [RegularExpression(@"^[0-9,]+$", ErrorMessage = "Permitido apenas números e virgulas.")]
        public string ValorMultaFixa { get; set; }

        [Required(ErrorMessage = "O valor da multa diária é obrigatório!")]
        [DisplayName("Valor multa diária")]
        [RegularExpression(@"^[0-9,]+$", ErrorMessage = "Permitido apenas números e virgulas.")]
        public string ValorMultaDiaria { get; set; }
    }
}
