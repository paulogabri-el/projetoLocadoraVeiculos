using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ProjetoLocadoraDeVeiculos.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult ErroReferencialCategoria()
        {
            return View();
        }

        public IActionResult ErroReferencialVeiculo()
        {
            return View();
        }

        public IActionResult ErroReferencialStatusLocacao()
        {
            return View();
        }

        public IActionResult ErroReferencialStatusVeiculo()
        {
            return View();
        }

        public IActionResult ErroReferencialCliente()
        {
            return View();
        }

        public IActionResult ErroReferencialTemporada()
        {
            return View();
        }
    }
}
