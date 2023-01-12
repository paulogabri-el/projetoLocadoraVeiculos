using Microsoft.AspNetCore.Mvc;
using ProjetoLocadoraDeVeiculos.Helper;
using ProjetoLocadoraDeVeiculos.Models;
using ProjetoLocadoraDeVeiculos.Repositorios;

namespace ProjetoLocadoraDeVeiculos.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuariorepositorio;

        private readonly ISessao _sessao;
        public LoginController(IUsuarioRepositorio usuariorepositorio, ISessao sessao) 
        {
            _usuariorepositorio = usuariorepositorio;
            _sessao = sessao;
        }
        public IActionResult Index()
        {
            // Se o usuário estiver logado, redireciona direto para a home.
            if (_sessao.BuscarSessaoUsuario() != null) return RedirectToAction("Index", "Home");

            return View();
        }

        public IActionResult Sair()
        {
            _sessao.RemoverSessaoUsuario();
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult Entrar(Login login)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Usuario usuario = _usuariorepositorio.BuscarPorEmail(login.Email);

                    if(usuario != null)
                    {
                        if (usuario.SenhaValida(login.Senha))
                        {
                            _sessao.CriarSessaoUsuario(usuario);
                            return RedirectToAction("Index", "Home");
                        }

                        TempData["MensagemErro"] = $"Senha do usuário é inválida. Por favor, tente novamente.";

                    }
                    TempData["MensagemErro"] = $"Usuário e/ou senha inválido(s). Por favor, tente novamente.";
                }

                return View("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não conseguimos realizar o seu login, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
