using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using CampEventos.Domain;
using CampEventos.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CampEventos.Persistence.Contextos
{
    public class CampEventosContext : IdentityDbContext<User, Role, int, 
                                      IdentityUserClaim<int>, 
                                      UserRole, 
                                      IdentityUserLogin<int>, 
                                      IdentityRoleClaim<int>, 
                                      IdentityUserToken<int>>
    {
        public CampEventosContext(DbContextOptions<CampEventosContext> options) : base(options) {}
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<Apresentador> Apresentadores { get; set; }
        public DbSet<ApresentadorEvento> ApresentadoresEventos { get; set; }
        public DbSet<RedeSocial> RedesSociais { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>(userRole =>
                {
                    userRole.HasKey(ur => new {ur.UserId, ur.RoleId});

                    userRole.HasOne(ur => ur.Role)
                            .WithMany(r => r.UserRoles)
                            .HasForeignKey(ur => ur.RoleId)
                            .IsRequired();
                }
            );
            modelBuilder.Entity<ApresentadorEvento>()
                .HasKey(AP => new { AP.EventoId, AP.ApresentadorId });

            modelBuilder.Entity<Evento>()
                .HasMany(e => e.RedesSociais)
                .WithOne(rs => rs.Evento)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Apresentador>()
                .HasMany(e => e.RedesSociais)
                .WithOne(rs => rs.Apresentador)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}