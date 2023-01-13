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
    public class LocacoesController : Controller
    {
        private readonly ProjetoLocadoraDeVeiculosContext _context;

        public LocacoesController(ProjetoLocadoraDeVeiculosContext context)
        {
            _context = context;
        }

        // GET: Locacoes
        public async Task<IActionResult> Index()
        {
            var projetoLocadoraDeVeiculosContext = _context.Locacao.Include(l => l.Cliente).Include(l => l.StatusLocacao).Include(l => l.Temporada).Include(l => l.Veiculo);
            var locacao = await projetoLocadoraDeVeiculosContext.ToListAsync();
            return View(locacao);
        }

        // GET: Locacoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Locacao == null)
            {
                return NotFound();
            }

            var locacao = await _context.Locacao
                .Include(l => l.Cliente)
                .Include(l => l.StatusLocacao)
                .Include(l => l.Temporada)
                .Include(l => l.Veiculo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (locacao == null)
            {
                return NotFound();
            }

            return View(locacao);
        }

        // GET: Locacoes/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Nome");
            ViewData["StatusLocacaoId"] = new SelectList(_context.StatusLocacao, "Id", "Nome");
            ViewData["TemporadaId"] = new SelectList(_context.Temporada, "Id", "Nome");
            var result = _context.Veiculo.Where(x => x.StatusVeiculoId == 1);
            ViewData["VeiculoId"] = new SelectList(result, "Id", "Nome");
            return View();
        }

        // POST: Locacoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClienteId,VeiculoId,StatusLocacaoId,TemporadaId,DataLocacao,DataEntrega,QtdDiasAlugados,QtdRenovacoes,ValorDiaria,DataCadastro,DataAlteracao")] LocacaoViewModel locacao)
        {
            if (ModelState.IsValid)
            {
                var newLoc = new Locacao()
                {
                    ClienteId = locacao.ClienteId,
                    StatusLocacaoId = locacao.StatusLocacaoId,
                    TemporadaId= locacao.TemporadaId,
                    ValorDiaria= locacao.ValorDiaria,
                    VeiculoId= locacao.VeiculoId,
                    QtdDiasAlugados = locacao.QtdDiasAlugados,
                    QtdRenovacoes = 0,
                    DataEntrega = locacao.DataEntrega,
                    DataLocacao = locacao.DataLocacao,
                    DataCadastro = DateTime.Now

                };
                _context.Add(newLoc);
                TimeSpan qtddias = DateTime.Now - newLoc.DataLocacao;
                newLoc.QtdDiasAlugados = qtddias.Days;
                var valorTotal = (newLoc.DataEntrega - newLoc.DataLocacao).Days * newLoc.ValorDiaria;
                newLoc.ValorTotal = valorTotal;
                _context.Add(newLoc);
                if(newLoc.StatusLocacaoId == 3)
                {
                    var vec = await _context.Veiculo.FindAsync(newLoc.VeiculoId);
                    vec.StatusVeiculoId = 2;
                    _context.Update(vec);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Nome", locacao.ClienteId);
            ViewData["StatusLocacaoId"] = new SelectList(_context.StatusLocacao, "Id", "Nome", locacao.StatusLocacaoId);
            ViewData["TemporadaId"] = new SelectList(_context.Temporada, "Id", "Nome", locacao.TemporadaId);
            ViewData["VeiculoId"] = new SelectList(_context.Veiculo, "Id", "Nome", locacao.VeiculoId);
            return View(locacao);
        }

        // GET: Locacoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Locacao == null)
            {
                return NotFound();
            }

            var locacao = await _context.Locacao.FindAsync(id);
            if (locacao == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Nome", locacao.ClienteId);
            ViewData["StatusLocacaoId"] = new SelectList(_context.StatusLocacao, "Id", "Nome", locacao.StatusLocacaoId);
            ViewData["TemporadaId"] = new SelectList(_context.Temporada, "Id", "Nome", locacao.TemporadaId);
            ViewData["VeiculoId"] = new SelectList(_context.Veiculo, "Id", "Nome", locacao.VeiculoId);
            return View(locacao);
        }

        // POST: Locacoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClienteId,VeiculoId,StatusLocacaoId,TemporadaId,DataLocacao,DataEntrega,QtdDiasAlugados,QtdRenovacoes,ValorDiaria,DataCadastro,DataAlteracao")] LocacaoViewModel locacao)
        {
            if (id != locacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var editLoc = await _context.Locacao.FindAsync(id);
                    editLoc.ClienteId = locacao.ClienteId;
                    editLoc.StatusLocacaoId = locacao.StatusLocacaoId;
                    editLoc.TemporadaId = locacao.TemporadaId;
                    editLoc.ValorDiaria= locacao.ValorDiaria;
                    editLoc.VeiculoId= locacao.VeiculoId;
                    editLoc.DataEntrega = locacao.DataEntrega;
                    editLoc.DataAlteracao = DateTime.Now;
                    TimeSpan qtddias = DateTime.Now - editLoc.DataLocacao;
                    editLoc.QtdDiasAlugados = qtddias.Days;
                    if (editLoc.QtdDiasAlugados < 0)
                    {
                        editLoc.QtdDiasAlugados = 0;
                    }

                    var vec = await _context.Veiculo.FindAsync(editLoc.VeiculoId);
                    
                    if (editLoc.StatusLocacaoId == 3)
                    {
                        vec.StatusVeiculoId = 2;
                        _context.Update(vec);
                    }
                    var valorTotal = (editLoc.DataEntrega - editLoc.DataLocacao).Days * editLoc.ValorDiaria;

                    if (editLoc.StatusLocacaoId == 2)
                    {
                        editLoc.DataEntrega = DateTime.Now;
                        vec.StatusVeiculoId = 3;
                        _context.Update(vec);
                    }
                    editLoc.ValorTotal = valorTotal;
                    _context.Update(editLoc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocacaoExists(locacao.Id))
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
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Nome", locacao.ClienteId);
            ViewData["StatusLocacaoId"] = new SelectList(_context.StatusLocacao, "Id", "Nome", locacao.StatusLocacaoId);
            ViewData["TemporadaId"] = new SelectList(_context.Temporada, "Id", "Nome", locacao.TemporadaId);
            ViewData["VeiculoId"] = new SelectList(_context.Veiculo, "Id", "Nome", locacao.VeiculoId);
            return View(locacao);
        }

        // GET: Locacoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Locacao == null)
            {
                return NotFound();
            }

            var locacao = await _context.Locacao
                .Include(l => l.Cliente)
                .Include(l => l.StatusLocacao)
                .Include(l => l.Temporada)
                .Include(l => l.Veiculo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (locacao == null)
            {
                return NotFound();
            }

            return View(locacao);
        }

        // POST: Locacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Locacao == null)
            {
                return Problem("Entity set 'ProjetoLocadoraDeVeiculosContext.Locacao'  is null.");
            }
            var locacao = await _context.Locacao.FindAsync(id);
            if (locacao != null)
            {
                _context.Locacao.Remove(locacao);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocacaoExists(int id)
        {
          return _context.Locacao.Any(e => e.Id == id);
        }
    }
}
