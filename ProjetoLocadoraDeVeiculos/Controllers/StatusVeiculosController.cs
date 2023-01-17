using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoLocadoraDeVeiculos.Data;
using ProjetoLocadoraDeVeiculos.Models;
using ProjetoLocadoraDeVeiculos.Models.ViewModels;

namespace ProjetoLocadoraDeVeiculos.Controllers
{
    public class StatusVeiculosController : Controller
    {
        private readonly ProjetoLocadoraDeVeiculosContext _context;

        public StatusVeiculosController(ProjetoLocadoraDeVeiculosContext context)
        {
            _context = context;
        }

        // GET: StatusVeiculos
        public async Task<IActionResult> Index()
        {
              return View(await _context.StatusVeiculo.ToListAsync());
        }

        // GET: StatusVeiculos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StatusVeiculo == null)
            {
                return NotFound();
            }

            var statusVeiculo = await _context.StatusVeiculo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (statusVeiculo == null)
            {
                return NotFound();
            }

            return View(statusVeiculo);
        }

        // GET: StatusVeiculos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StatusVeiculos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] StatusVeiculoViewModel statusVeiculo)
        {
            if (ModelState.IsValid)
            {
                var newStatusCar = new StatusVeiculo()
                {
                    Id = statusVeiculo.Id,
                    Nome = statusVeiculo.Nome,
                    DataCadastro = DateTime.Now
                };
                
                _context.Add(newStatusCar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(statusVeiculo);
        }

        // GET: StatusVeiculos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StatusVeiculo == null)
            {
                return NotFound();
            }

            var statusVeiculo = await _context.StatusVeiculo.FindAsync(id);
            if (statusVeiculo == null)
            {
                return NotFound();
            }
            return View(statusVeiculo);
        }

        // POST: StatusVeiculos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] StatusVeiculoViewModel statusVeiculo)
        {
            if (id != statusVeiculo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var editStatusCar = await _context.StatusVeiculo.FindAsync(id);
                    editStatusCar.Nome = statusVeiculo.Nome;
                    editStatusCar.DataAlteracao = DateTime.Now;
                    _context.Update(editStatusCar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatusVeiculoExists(statusVeiculo.Id))
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
            return View(statusVeiculo);
        }

        // GET: StatusVeiculos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StatusVeiculo == null)
            {
                return NotFound();
            }

            var statusVeiculo = await _context.StatusVeiculo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (statusVeiculo == null)
            {
                return NotFound();
            }

            return View(statusVeiculo);
        }

        // POST: StatusVeiculos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                if (_context.StatusVeiculo == null)
                {
                    return Problem("Entity set 'ProjetoLocadoraDeVeiculosContext.StatusVeiculo'  is null.");
                }
                var statusVeiculo = await _context.StatusVeiculo.FindAsync(id);
                if (statusVeiculo != null)
                {
                    _context.StatusVeiculo.Remove(statusVeiculo);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception erro)
            {

                return RedirectToAction("ErroReferencialStatusVeiculo", "Error");
            }
        }

        private bool StatusVeiculoExists(int id)
        {
          return _context.StatusVeiculo.Any(e => e.Id == id);
        }
    }
}
