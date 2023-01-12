using ProjetoLocadoraDeVeiculos.Models;

namespace ProjetoLocadoraDeVeiculos.Repositorios
{
    public interface IUsuarioRepositorio
    {
        Usuario BuscarPorEmail(string email);
        List<Usuario> BuscarTodos();
        Usuario BuscarPorID(int id);
    }
}
