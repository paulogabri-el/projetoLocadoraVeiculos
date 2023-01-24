using Microsoft.EntityFrameworkCore;
using ProjetoLocadoraDeVeiculos.Helper;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ProjetoLocadoraDeVeiculos.Models
{
    [Index(nameof(Cpf), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    public class Usuario
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        [DisplayName("Nome do usuário")]
        [RegularExpression(@"^[a-zA-Zà-úÀ-Ú ]+$", ErrorMessage = "Você inseriu caracteres inválidos para o nome.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        [DisplayName("CPF")]
        public string Cpf { get; set; }
        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        [DisplayName("E-Mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Campo de preenchimento obrigatório.")]
        [DisplayName("Senha")]
        public string Senha { get; set; }
        [DisplayName("Data de cadastro")]
        [DataType(DataType.Date)]
        public DateTime? DataCadastro { get; set; }
        [DisplayName("Data de alteração")]
        [DataType(DataType.Date)]
        public DateTime? DataAlteracao { get; set; }

        public bool SenhaValida(string senha)
        {
            return Senha == senha.GerarHash();
        }

        public void SetSenhaHash()
        {
            Senha = Senha.GerarHash();
        }

    }
}
