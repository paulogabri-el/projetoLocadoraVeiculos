using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
            //Exibir somente os status "Agendada" e "Em andamento" no momento do cadastro da locação.
            var resultStat = _context.StatusLocacao.Where(x => x.Id == 1 || x.Id == 3);
            ViewData["StatusLocacaoId"] = new SelectList(resultStat, "Id", "Nome");
            ViewData["TemporadaId"] = new SelectList(_context.Temporada, "Id", "Nome");
            //Exibir somente veículos com status "Disponível" no momento do cadastro da locação.
            var resultVec = _context.Veiculo.Where(x => x.StatusVeiculoId == 1);
            ViewData["VeiculoId"] = new SelectList(resultVec, "Id", "Nome");
            return View();
        }

        // POST: Locacoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClienteId,VeiculoId,StatusLocacaoId,TemporadaId,DataLocacao,DataEntrega,QtdDiasAlugados,QtdRenovacoes,ValorDiaria,ValorMultaDiaria,ValorMultaFixa,DataCadastro,DataAlteracao")] LocacaoViewModel locacao)
        {
            if (ModelState.IsValid)
            {
                var newLoc = new Locacao()
                {
                    ClienteId = locacao.ClienteId,
                    StatusLocacaoId = locacao.StatusLocacaoId,
                    TemporadaId = locacao.TemporadaId,
                    ValorDiaria = locacao.ValorDiaria,
                    ValorMultaDiaria = locacao.ValorMultaDiaria,
                    ValorMultaFixa = locacao.ValorMultaFixa,
                    VeiculoId = locacao.VeiculoId,
                    QtdDiasAlugados = locacao.QtdDiasAlugados,
                    QtdRenovacoes = 0,
                    DataEntrega = locacao.DataEntrega,
                    DataEntregaOriginal = locacao.DataEntrega,
                    DataLocacao = locacao.DataLocacao,
                    DataCadastro = DateTime.Now

                };
                _context.Add(newLoc);
                TimeSpan qtddias = DateTime.Now - newLoc.DataLocacao;
                newLoc.QtdDiasAlugados = qtddias.Days;

                //Validação para não setar a quantidade de dias negativo.
                if (newLoc.QtdDiasAlugados < 0)
                {
                    newLoc.QtdDiasAlugados = 0;
                }

                //Calculo estipulado do valor total da locação, juros vão ser calculados somente quando a locação for alterada para o status "Finalizada".
                var valorTotal = (newLoc.DataEntrega - newLoc.DataLocacao).Days * newLoc.ValorDiaria;
                newLoc.ValorTotal = valorTotal;

                //Validação para não permitir alugar o veículo por mais de 30 dias.
                if((newLoc.DataEntrega - newLoc.DataLocacao).Days > 30)
                {
                    TempData["MensagemErro"] = $"Você não pode alugar um veículo por mais de 30 dias.";
                }
                else 
                {
                    _context.Add(newLoc);
                    //Validação para alterar o status do veículo para "Alugado" caso o status da locação seja definido como "Em andamento".
                    if (newLoc.StatusLocacaoId == 3)
                    {
                        var vec = await _context.Veiculo.FindAsync(newLoc.VeiculoId);
                        vec.StatusVeiculoId = 2;
                        _context.Update(vec);
                    }

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Create));
            }
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Nome", locacao.ClienteId);
            ViewData["StatusLocacaoId"] = new SelectList(_context.StatusLocacao, "Id", "Nome", locacao.StatusLocacaoId);
            ViewData["TemporadaId"] = new SelectList(_context.Temporada, "Id", "Nome", locacao.TemporadaId);
            ViewData["VeiculoId"] = new SelectList(_context.Veiculo, "Id", "Nome", locacao.VeiculoId);
            return View(locacao);
        }

        public ActionResult Teste(int idT, int idV)
        {
            var vlTemp = _context.Temporada.Find(idT);
            var vlVec = _context.Veiculo.Find(idV);
            var result = (vlTemp.PercentualAcrescerDiaria * vlVec.ValorDiaria / 100) + vlVec.ValorDiaria;

            return View(result);
        }


        //public decimal vltotal(int idT, int idV)
        //{
        //    var vlTemp = _context.Temporada.Find(idT);
        //    var vlVec = _context.Veiculo.Find(idV);

        //    return (vlVec.ValorDiaria * vlTemp.PercentualAcrescerDiaria / 100) + vlVec.ValorDiaria;
        //}


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
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClienteId,VeiculoId,StatusLocacaoId,TemporadaId,DataLocacao,DataEntrega,QtdDiasAlugados,QtdRenovacoes,ValorDiaria,ValorMultaDiaria,ValorMultaFixa,DataCadastro,DataAlteracao")] LocacaoViewModel locacao)
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
                    var aux = editLoc.DataLocacao;
                    var aux2 = editLoc.DataEntrega;
                    var aux3 = editLoc.StatusLocacaoId;
                    editLoc.ClienteId = locacao.ClienteId;
                    editLoc.StatusLocacaoId = locacao.StatusLocacaoId;
                    editLoc.TemporadaId = locacao.TemporadaId;
                    editLoc.ValorDiaria = locacao.ValorDiaria;
                    editLoc.ValorMultaDiaria = locacao.ValorMultaDiaria;
                    editLoc.ValorMultaFixa = locacao.ValorMultaFixa;
                    editLoc.VeiculoId= locacao.VeiculoId;
                    editLoc.DataEntrega = locacao.DataEntrega;
                    editLoc.DataAlteracao = DateTime.Now;

                    if (aux3 == 1 && editLoc.StatusLocacaoId == 2)
                    {
                        TempData["MensagemErro"] = $"Locações com status 'Agendada' só podem ser alteradas para 'Em andamento' ou 'Cancelada'.";
                    }

                    //Validação para não permitir o veículo ser alugado por mais de 30 dias ou ter mais de 3 renovações.
                    else if ((editLoc.DataEntrega - editLoc.DataLocacao).Days > 30 || (editLoc.QtdRenovacoes == 3 && editLoc.DataEntrega > aux2))
                    {
                        TempData["MensagemErro2"] = $"Você não pode ter mais de 3 renovações ou alugar um carro por mais de 30 dias.";
                    }


                    else if (aux3 != 1 && editLoc.StatusLocacaoId == 4)
                    {
                        TempData["MensagemErro3"] = $"Somente locações com status 'Agendada' podem ser canceladas.";
                    }


                    else if (aux3 == 3 && (editLoc.StatusLocacaoId == 1 || editLoc.StatusLocacaoId == 4))
                    {
                        TempData["MensagemErro4"] = $"Locações com status 'Em andamento' só podem ter o status alterado para 'Finalizada'.";
                    }


                    else {
                    var vec = await _context.Veiculo.FindAsync(editLoc.VeiculoId);

                        //Validação para alterar o status do veículo para "Alugado" caso o status da locação seja definido como "Em andamento".
                        if (editLoc.StatusLocacaoId == 3)
                        {
                            vec.StatusVeiculoId = 2;
                            _context.Update(vec);
                        }

                        //Validação para calcular os valores totais da locação caso o status seja definido como "Finalizada".
                        if (editLoc.StatusLocacaoId == 2)
                        {
                            var difDias = (DateTime.Now - aux2).Days;
                            editLoc.DataEntrega = DateTime.Now;
                            vec.StatusVeiculoId = 3;
                            _context.Update(vec);

                            //Calculo caso a devolução esteja em dias.
                            if(difDias <= 0) 
                            {
                                var valorTotal = (editLoc.DataEntrega - editLoc.DataLocacao).Days * editLoc.ValorDiaria;
                                editLoc.ValorTotal = valorTotal;
                            }

                            //Calculo caso a devolução esteja em atraso.
                            else
                            {
                                var days = (DateTime.Now - aux2).Days;
                                var valorTotal = ((aux2 - aux).Days * editLoc.ValorDiaria) + (days * editLoc.ValorMultaDiaria) + editLoc.ValorMultaFixa;
                                editLoc.ValorTotal = valorTotal;
                            }
                            _context.Update(editLoc);
                            await _context.SaveChangesAsync();

                        }
                        else 
                        { 
                            var valorTotal = (editLoc.DataEntrega - editLoc.DataLocacao).Days * editLoc.ValorDiaria;
                            editLoc.ValorTotal = valorTotal;

                            //Validação para alterar o status do veículo para "Disponível" caso o status da locação seja definido como "Cancelada" e definir valor total como R$ 0,00.
                            if (editLoc.StatusLocacaoId == 4)
                            {
                                editLoc.ValorTotal = 0;
                                vec.StatusVeiculoId = 1;
                                _context.Update(vec);
                            }
                            _context.Update(editLoc);
                            await _context.SaveChangesAsync();
                        }
                        return RedirectToAction(nameof(Index));
                    }
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
                return RedirectToAction(nameof(Edit));
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
