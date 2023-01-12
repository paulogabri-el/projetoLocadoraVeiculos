using ProjetoLocadoraDeVeiculos.Models;

namespace ProjetoLocadoraDeVeiculos.Helper
{
    public interface ISessao
    {
        void CriarSessaoUsuario(Usuario usuario);
        void RemoverSessaoUsuario();
        Usuario BuscarSessaoUsuario();
    }
}
