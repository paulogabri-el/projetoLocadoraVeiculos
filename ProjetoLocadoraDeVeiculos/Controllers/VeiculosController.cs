using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class VeiculosController : Controller
    {
        private readonly ProjetoLocadoraDeVeiculosContext _context;

        public VeiculosController(ProjetoLocadoraDeVeiculosContext context)
        {
            _context = context;
        }

        // GET: Veiculos
        public async Task<IActionResult> Index([FromServices] ISessao _sessao)
        {
            if (_sessao.BuscarSessaoUsuario() == null) return RedirectToAction("Index", "Login");

            var projetoLocadoraDeVeiculosContext = _context.Veiculo.Include(v => v.CategoriaVeiculo).Include(v => v.StatusVeiculo);
            return View(await projetoLocadoraDeVeiculosContext.ToListAsync());
        }

        // GET: Veiculos/Details/5
        public async Task<IActionResult> Details([FromServices] ISessao _sessao, int? id)
        {
            if (_sessao.BuscarSessaoUsuario() == null) return RedirectToAction("Index", "Login");

            if (id == null || _context.Veiculo == null)
            {
                return NotFound();
            }

            var veiculo = await _context.Veiculo
                .Include(v => v.CategoriaVeiculo)
                .Include(v => v.StatusVeiculo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (veiculo == null)
            {
                return NotFound();
            }

            return View(veiculo);
        }

        // GET: Veiculos/Create
        public IActionResult Create([FromServices] ISessao _sessao)
        {
            if (_sessao.BuscarSessaoUsuario() == null) return RedirectToAction("Index", "Login");

            ViewData["CategoriaVeiculoId"] = new SelectList(_context.CategoriaVeiculo, "Id", "Nome");
            ViewData["StatusVeiculoId"] = new SelectList(_context.StatusVeiculo, "Id", "Nome");
            return View();
        }

        // POST: Veiculos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromServices] ISessao _sessao, [Bind("Id,Nome,CategoriaVeiculoId,Placa,StatusVeiculoId,ValorDiaria,ValorMultaFixa,ValorMultaDiaria,DataCadastro,DataAlteracao")] VeiculoViewModel veiculo)
        {
            if (_sessao.BuscarSessaoUsuario() == null) return RedirectToAction("Index", "Login");


            if (ModelState.IsValid)
            {
                var valorDiaria = Helper.Convert.ConvertStringDecimal(veiculo.ValorDiaria);
                var valorMultaFixa = Helper.Convert.ConvertStringDecimal(veiculo.ValorMultaFixa);
                var valorMultaDiaria = Helper.Convert.ConvertStringDecimal(veiculo.ValorMultaDiaria);
                var placa = Helper.Convert.RemoverCaracteresPlaca(veiculo.Placa);

                var newCar = new Veiculo()
                {
                    Id = veiculo.Id,
                    Nome = veiculo.Nome,
                    CategoriaVeiculoId = veiculo.CategoriaVeiculoId,
                    Placa = placa,
                    StatusVeiculoId = veiculo.StatusVeiculoId,
                    ValorDiaria = valorDiaria,
                    ValorMultaFixa = valorMultaFixa,
                    ValorMultaDiaria = valorMultaDiaria,
                    DataCadastro = DateTime.Now
                };
                _context.Add(newCar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaVeiculoId"] = new SelectList(_context.CategoriaVeiculo, "Id", "Nome", veiculo.CategoriaVeiculoId);
            ViewData["StatusVeiculoId"] = new SelectList(_context.StatusVeiculo, "Id", "Nome", veiculo.StatusVeiculoId);
            return View(veiculo);
        }

        // GET: Veiculos/Edit/5
        public async Task<IActionResult> Edit([FromServices] ISessao _sessao, int? id)
        {
            if (_sessao.BuscarSessaoUsuario() == null) return RedirectToAction("Index", "Login");

            if (id == null || _context.Veiculo == null)
            {
                return NotFound();
            }

            var veiculo = await _context.Veiculo.FindAsync(id);
            var valorDiaria = veiculo.ValorDiaria.ToString("F2", CultureInfo.InvariantCulture).Replace(".", ",");
            var valorMultaDiaria = veiculo.ValorMultaDiaria.ToString("F2", CultureInfo.InvariantCulture).Replace(".", ",");
            var valorMultaFixa = veiculo.ValorMultaFixa.ToString("F2", CultureInfo.InvariantCulture).Replace(".", ",");
            var placa = Helper.Convert.RemoverCaracteresPlaca(veiculo.Placa);

            var veiculoEdit = new VeiculoViewModel()
            {
                Nome = veiculo.Nome,
                CategoriaVeiculoId = veiculo.CategoriaVeiculoId,
                StatusVeiculoId = veiculo.StatusVeiculoId,
                Placa = placa,
                ValorDiaria = valorDiaria,
                ValorMultaDiaria = valorMultaDiaria,
                ValorMultaFixa = valorMultaFixa,

            };

            if (veiculo == null)
            {
                return NotFound();
            }
            ViewData["CategoriaVeiculoId"] = new SelectList(_context.CategoriaVeiculo, "Id", "Nome", veiculoEdit.CategoriaVeiculoId);
            ViewData["StatusVeiculoId"] = new SelectList(_context.StatusVeiculo, "Id", "Nome", veiculoEdit.StatusVeiculoId);
            return View(veiculoEdit);
        }

        // POST: Veiculos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromServices] ISessao _sessao, int id, [Bind("Id,Nome,CategoriaVeiculoId,Placa,StatusVeiculoId,ValorDiaria,ValorMultaFixa,ValorMultaDiaria,DataCadastro,DataAlteracao")] VeiculoViewModel veiculo)
        {
            if (_sessao.BuscarSessaoUsuario() == null) return RedirectToAction("Index", "Login");

            if (id != veiculo.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    var valorDiaria = Helper.Convert.ConvertStringDecimal(veiculo.ValorDiaria);
                    var valorMultaFixa = Helper.Convert.ConvertStringDecimal(veiculo.ValorMultaFixa);
                    var valorMultaDiaria = Helper.Convert.ConvertStringDecimal(veiculo.ValorMultaDiaria);


                    var editCar = await _context.Veiculo.FindAsync(id);
                    editCar.Nome = veiculo.Nome;
                    editCar.CategoriaVeiculoId = veiculo.CategoriaVeiculoId;
                    editCar.Placa = veiculo.Placa;
                    editCar.StatusVeiculoId = veiculo.StatusVeiculoId;
                    editCar.ValorDiaria = valorDiaria;
                    editCar.ValorMultaFixa = valorMultaFixa;
                    editCar.ValorMultaDiaria = valorMultaDiaria;
                    editCar.DataAlteracao = DateTime.Now;
                    _context.Update(editCar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VeiculoExists(veiculo.Id))
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
            ViewData["CategoriaVeiculoId"] = new SelectList(_context.CategoriaVeiculo, "Id", "Nome", veiculo.CategoriaVeiculoId);
            ViewData["StatusVeiculoId"] = new SelectList(_context.StatusVeiculo, "Id", "Nome", veiculo.StatusVeiculoId);
            return View(veiculo);
        }

        // GET: Veiculos/Delete/5
        public async Task<IActionResult> Delete([FromServices] ISessao _sessao, int? id)
        {
            if (_sessao.BuscarSessaoUsuario() == null) return RedirectToAction("Index", "Login");

            if (id == null || _context.Veiculo == null)
            {
                return NotFound();
            }

            var veiculo = await _context.Veiculo
                .Include(v => v.CategoriaVeiculo)
                .Include(v => v.StatusVeiculo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (veiculo == null)
            {
                return NotFound();
            }

            return View(veiculo);
        }

        // POST: Veiculos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([FromServices] ISessao _sessao, int id)
        {
            if (_sessao.BuscarSessaoUsuario() == null) return RedirectToAction("Index", "Login");

            try
            {
                if (_context.Veiculo == null)
                {
                    return Problem("Entity set 'ProjetoLocadoraDeVeiculosContext.Veiculo'  is null.");
                }
                var veiculo = await _context.Veiculo.FindAsync(id);
                if (veiculo != null)
                {
                    _context.Veiculo.Remove(veiculo);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception erro)
            {

                return RedirectToAction("ErroReferencialVeiculo", "Error");
            }
        }

        private bool VeiculoExists(int id)
        {
            return _context.Veiculo.Any(e => e.Id == id);
        }
    }
}
