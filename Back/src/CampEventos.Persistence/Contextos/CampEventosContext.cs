using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampEventos.Domain;
using Microsoft.EntityFrameworkCore;

namespace CampEventos.Persistence.Contextos
{
    public class CampEventosContext : DbContext{
        public CampEventosContext(DbContextOptions<CampEventosContext> options) : base(options) {}
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<Apresentador> Apresentadores { get; set; }
        public DbSet<ApresentadorEvento> ApresentadoresEventos { get; set; }
        public DbSet<RedeSocial> RedesSociais { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApresentadorEvento>()
            .HasKey(AP => new { AP.EventoId, AP.ApresentadorId });
        }
    }
}