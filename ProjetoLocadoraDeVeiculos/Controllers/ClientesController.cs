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
using DocumentValidator;
using System.Linq.Expressions;

namespace ProjetoLocadoraDeVeiculos.Controllers
{
    public class ClientesController : Controller
    {
        private readonly ProjetoLocadoraDeVeiculosContext _context;

        public ClientesController(ProjetoLocadoraDeVeiculosContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
              return View(await _context.Cliente.ToListAsync());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cliente == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Cpf,Cnh,DataNascimento,DataCadastro,DataAlteracao")] ClienteViewModel cliente)
        {
                if (ModelState.IsValid)
                {
                    var newCliente = new Cliente()
                    {
                        Id = cliente.Id,
                        Nome = cliente.Nome,
                        Cpf = cliente.Cpf,
                        Cnh = cliente.Cnh,
                        DataNascimento = cliente.DataNascimento,
                        DataCadastro = DateTime.Now
                    };
                        _context.Add(newCliente);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                return View(cliente);
        }
                

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cliente == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Cpf,Cnh,DataNascimento,DataCadastro,DataAlteracao")] ClienteViewModel cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var editCliente = await _context.Cliente.FindAsync(id);
                    editCliente.Id = cliente.Id;
                    editCliente.Nome = cliente.Nome;
                    editCliente.Cpf = cliente.Cpf;
                    editCliente.Cnh = cliente.Cnh;
                    editCliente.DataNascimento = cliente.DataNascimento;
                    editCliente.DataAlteracao = DateTime.Now;
                    _context.Update(editCliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id))
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
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cliente == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                if (_context.Cliente == null)
                {
                    return Problem("Entity set 'ProjetoLocadoraDeVeiculosContext.Cliente'  is null.");
                }
                var cliente = await _context.Cliente.FindAsync(id);
                if (cliente != null)
                {
                    _context.Cliente.Remove(cliente);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception erro)
            {

                return RedirectToAction("ErroReferencialCliente", "Error");
            }
        }

        private bool ClienteExists(int id)
        {
          return _context.Cliente.Any(e => e.Id == id);
        }
    }
}
