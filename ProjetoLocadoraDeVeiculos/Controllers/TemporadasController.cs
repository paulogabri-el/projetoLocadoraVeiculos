using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoLocadoraDeVeiculos.Data;
using ProjetoLocadoraDeVeiculos.Helper;
using ProjetoLocadoraDeVeiculos.Models;
using ProjetoLocadoraDeVeiculos.Models.ViewModels;

namespace ProjetoLocadoraDeVeiculos.Controllers
{
    public class TemporadasController : Controller
    {
        private readonly ProjetoLocadoraDeVeiculosContext _context;

        public TemporadasController(ProjetoLocadoraDeVeiculosContext context)
        {
            _context = context;
        }

        // GET: Temporadas
        public async Task<IActionResult> Index([FromServices] ISessao _sessao)
        {
            if (_sessao.BuscarSessaoUsuario() == null) return RedirectToAction("Index", "Login");

            return View(await _context.Temporada.ToListAsync());
        }

        // GET: Temporadas/Details/5
        public async Task<IActionResult> Details([FromServices] ISessao _sessao, int? id)
        {
            if (_sessao.BuscarSessaoUsuario() == null) return RedirectToAction("Index", "Login");

            if (id == null || _context.Temporada == null)
            {
                return NotFound();
            }

            var temporada = await _context.Temporada
                .FirstOrDefaultAsync(m => m.Id == id);
            if (temporada == null)
            {
                return NotFound();
            }

            return View(temporada);
        }

        // GET: Temporadas/Create
        public IActionResult Create([FromServices] ISessao _sessao)
        {
            if (_sessao.BuscarSessaoUsuario() == null) return RedirectToAction("Index", "Login");

            return View();
        }

        // POST: Temporadas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromServices] ISessao _sessao, [Bind("Id,Nome,PercentualAcrescerDiaria,PercentualAcrescerMultaFixa,PercentualAcrescerMultaDiaria")] TemporadaViewModel temporada)
        {
            if (_sessao.BuscarSessaoUsuario() == null) return RedirectToAction("Index", "Login");

            if (ModelState.IsValid)
            {
                var newTemp = new Temporada()
                {
                    Id = temporada.Id,
                    Nome = temporada.Nome,
                    PercentualAcrescerDiaria = temporada.PercentualAcrescerDiaria,
                    PercentualAcrescerMultaFixa = temporada.PercentualAcrescerMultaFixa,
                    PercentualAcrescerMultaDiaria = temporada.PercentualAcrescerMultaDiaria,
                    DataCadastro = DateTime.Now
                };
                _context.Add(newTemp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(temporada);
        }

        // GET: Temporadas/Edit/5
        public async Task<IActionResult> Edit([FromServices] ISessao _sessao, int? id)
        {
            if (_sessao.BuscarSessaoUsuario() == null) return RedirectToAction("Index", "Login");

            if (id == null || _context.Temporada == null)
            {
                return NotFound();
            }

            var temporada = await _context.Temporada.FindAsync(id);
            if (temporada == null)
            {
                return NotFound();
            }
            return View(temporada);
        }

        // POST: Temporadas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromServices] ISessao _sessao, int id, [Bind("Id,Nome,PercentualAcrescerDiaria,PercentualAcrescerMultaFixa,PercentualAcrescerMultaDiaria")] TemporadaViewModel temporada)
        {
            if (_sessao.BuscarSessaoUsuario() == null) return RedirectToAction("Index", "Login");

            if (id != temporada.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var editTemp = await _context.Temporada.FindAsync(id);
                    editTemp.Id = temporada.Id;
                    editTemp.Nome = temporada.Nome;
                    editTemp.PercentualAcrescerDiaria = temporada.PercentualAcrescerDiaria;
                    editTemp.PercentualAcrescerMultaFixa = temporada.PercentualAcrescerMultaFixa;
                    editTemp.PercentualAcrescerMultaDiaria = temporada.PercentualAcrescerMultaDiaria;
                    editTemp.DataAlteracao = DateTime.Now;
                    _context.Update(editTemp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TemporadaExists(temporada.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(temporada);
        }

        // GET: Temporadas/Delete/5
        public async Task<IActionResult> Delete([FromServices] ISessao _sessao, int? id)
        {
            if (_sessao.BuscarSessaoUsuario() == null) return RedirectToAction("Index", "Login");

            if (id == null || _context.Temporada == null)
            {
                return NotFound();
            }

            var temporada = await _context.Temporada
                .FirstOrDefaultAsync(m => m.Id == id);
            if (temporada == null)
            {
                return NotFound();
            }

            return View(temporada);
        }

        // POST: Temporadas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([FromServices] ISessao _sessao, int id)
        {
            if (_sessao.BuscarSessaoUsuario() == null) return RedirectToAction("Index", "Login");

            try
            {
                if (_context.Temporada == null)
                {
                    return Problem("Entity set 'ProjetoLocadoraDeVeiculosContext.Temporada'  is null.");
                }
                var temporada = await _context.Temporada.FindAsync(id);
                if (temporada != null)
                {
                    _context.Temporada.Remove(temporada);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception erro)
            {

                return RedirectToAction("ErroReferencialTemporada", "Error");
            }
        }

        private bool TemporadaExists(int id)
        {
            return _context.Temporada.Any(e => e.Id == id);
        }
    }
}
