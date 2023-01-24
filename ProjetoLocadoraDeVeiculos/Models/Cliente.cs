using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using DocumentValidator;
using Microsoft.EntityFrameworkCore;

namespace ProjetoLocadoraDeVeiculos.Models
{
    [Index(nameof(Cpf), IsUnique = true)]
    [Index(nameof(Cnh), IsUnique = true)]
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório.")]
        [DisplayName("Nome do cliente")]
        [RegularExpression(@"^[a-zA-Zà-úÀ-Ú ]+$", ErrorMessage = "Você inseriu caracteres inválidos para o nome.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "CPF é obrigatório.")]
        [DisplayName("CPF")]
        public string Cpf { get; set; }
        [Required(ErrorMessage = "CNH é obrigatório.")]
        [DisplayName("CNH")]
        public string Cnh { get; set; }
        [Required(ErrorMessage = "O seu cliente precisa ter mais de 18 anos.")]
        [DisplayName("Data de nascimento")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }
        [DisplayName("Data de cadastro")]
        [DataType(DataType.Date)]
        public DateTime? DataCadastro { get; set; }
        [DisplayName("Data de alteração")]
        [DataType(DataType.Date)]
        public DateTime? DataAlteracao { get; set; }
        public ICollection<Locacao> Locacoes { get; set; } = new List<Locacao>();
    }
}
