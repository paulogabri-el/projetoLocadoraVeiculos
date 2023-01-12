using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjetoLocadoraDeVeiculos.Models;

namespace ProjetoLocadoraDeVeiculos.Data
{
    public class ProjetoLocadoraDeVeiculosContext : DbContext
    {
        public ProjetoLocadoraDeVeiculosContext (DbContextOptions<ProjetoLocadoraDeVeiculosContext> options)
            : base(options)
        {
        }

        public DbSet<CategoriaVeiculo> CategoriaVeiculo { get; set; } = default!;
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Locacao> Locacao { get; set; }
        public DbSet<Temporada> Temporada { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Veiculo> Veiculo { get; set; }
        public DbSet<StatusVeiculo> StatusVeiculo { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<StatusLocacao> StatusLocacao { get; set; }

        
    }
}
