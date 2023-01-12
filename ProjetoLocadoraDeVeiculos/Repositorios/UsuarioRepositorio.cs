using ProjetoLocadoraDeVeiculos.Data;
using ProjetoLocadoraDeVeiculos.Models;

namespace ProjetoLocadoraDeVeiculos.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly ProjetoLocadoraDeVeiculosContext _context;

        public UsuarioRepositorio(ProjetoLocadoraDeVeiculosContext context)
        {
            this._context = context;
        }

        public Usuario BuscarPorEmail(string email)
        {
            return _context.Usuario.FirstOrDefault(x => x.Email.ToUpper() == email.ToUpper());
        }

        public Usuario BuscarPorID(int id)
        {
            return _context.Usuario.FirstOrDefault(x => x.Id == id);
        }

        public List<Usuario> BuscarTodos()
        {
            return _context.Usuario.ToList();
        }

    }
}
