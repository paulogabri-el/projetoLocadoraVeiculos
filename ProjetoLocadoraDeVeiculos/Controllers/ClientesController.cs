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
using ProjetoLocadoraDeVeiculos.Helper;

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
        public async Task<IActionResult> Index([FromServices] ISessao _sessao)
        {
            return View(await _context.Cliente.ToListAsync());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details([FromServices] ISessao _sessao, int? id)
        {
            if (_sessao.BuscarSessaoUsuario() == null) return RedirectToAction("Index", "Login");

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
        public IActionResult Create([FromServices] ISessao _sessao)
        {
            if (_sessao.BuscarSessaoUsuario() == null) return RedirectToAction("Index", "Login");

            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromServices] ISessao _sessao, [Bind("Id,Nome,Cpf,Cnh,DataNascimento,DataCadastro,DataAlteracao")] ClienteViewModel cliente)
        {
            if (_sessao.BuscarSessaoUsuario() == null) return RedirectToAction("Index", "Login");

            //var clienteExiste = _context.Cliente.Where(x => x.Cpf == cliente.Cpf);

            //if (clienteExiste != null)
            //{
            //    var clienteExistente = clienteExiste.FirstOrDefault();
            //    if (clienteExistente != null)
            //    {
            //        TempData["MensagemClienteExistente"] = $"Já existe uma pessoa cadastrada com esse CPF! Nome: {clienteExistente.Nome}";

            //        return RedirectToAction(nameof(Create));
            //    }
                
            //}

            var cpfValido = CpfValidation.Validate(cliente.Cpf);
            var cnhValida = CnhValidation.Validate(cliente.Cnh);
            var idadeValida = (DateTime.Now - cliente.DataNascimento).Days >= 6570;

            try
            {
                if (ModelState.IsValid && cpfValido && cnhValida && idadeValida)
                {
                    var cpf = Helper.Convert.RemoverCaracteresCpf(cliente.Cpf);

                    var newCliente = new Cliente()
                    {
                        Id = cliente.Id,
                        Nome = cliente.Nome,
                        Cpf = cpf,
                        Cnh = cliente.Cnh,
                        DataNascimento = cliente.DataNascimento,
                        DataCadastro = DateTime.Now
                    };
                    _context.Add(newCliente);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    if (!cpfValido)
                        TempData["MensagemErroCpf"] = $"O CPF informado não é valido!";

                    if (!cnhValida)
                        TempData["MensagemErroCnh"] = $"A CNH informada não é valida!";

                    if (!idadeValida)
                        TempData["MensagemErroIdade"] = $"Só é permitido pessoas com mais de 18 anos!";

                    return RedirectToAction(nameof(Create));
                }
            }
            catch (Exception)
            {

                TempData["MensagemErro"] = $"O CPF ou a CNH que está tentando utilizar já está sendo utilizado por outro cliente!";

                return RedirectToAction("Create");
            }
            
        }


        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit([FromServices] ISessao _sessao, int? id)
        {
            if (_sessao.BuscarSessaoUsuario() == null) return RedirectToAction("Index", "Login");

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
        public async Task<IActionResult> Edit([FromServices] ISessao _sessao, int id, [Bind("Id,Nome,Cpf,Cnh,DataNascimento,DataCadastro,DataAlteracao")] ClienteViewModel cliente)
        {
            if (_sessao.BuscarSessaoUsuario() == null) return RedirectToAction("Index", "Login");

            if (id != cliente.Id)
            {
                return NotFound();
            }

            
            var editCliente = await _context.Cliente.FindAsync(id);

            var cpfValido = CpfValidation.Validate(cliente.Cpf);
            var cnhValida = CnhValidation.Validate(cliente.Cnh);
            var idadeValida = (DateTime.Now - cliente.DataNascimento).Days >= 6570;

            try
            {
                if (ModelState.IsValid && cpfValido && cnhValida && idadeValida)
                {
                    try
                    {
                        var cpf = Helper.Convert.RemoverCaracteresCpf(cliente.Cpf);

                        editCliente.Id = cliente.Id;
                        editCliente.Nome = cliente.Nome;
                        editCliente.Cpf = cpf;
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
                else
                {
                    if (!cpfValido)
                        TempData["MensagemErroCpf"] = $"O CPF informado não é valido!";

                    if (!cnhValida)
                        TempData["MensagemErroCnh"] = $"A CNH informada não é valida!";

                    if (!idadeValida)
                        TempData["MensagemErroIdade"] = $"Só é permitido pessoas com mais de 18 anos!";

                    return RedirectToAction(nameof(Edit));
                }
            }
            catch (Exception)
            {

                TempData["MensagemErro"] = $"O CPF ou a CNH que está tentando utilizar já está sendo utilizado por outro cliente!";

                return RedirectToAction("Edit");
            }
            

        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete([FromServices] ISessao _sessao, int? id)
        {
            if (_sessao.BuscarSessaoUsuario() == null) return RedirectToAction("Index", "Login");

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
        public async Task<IActionResult> DeleteConfirmed([FromServices] ISessao _sessao, int id)
        {
            if (_sessao.BuscarSessaoUsuario() == null) return RedirectToAction("Index", "Login");

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
