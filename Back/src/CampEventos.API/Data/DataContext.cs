using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampEventos.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CampEventos.API
{
    public class DataContext : DbContext{
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}
        public DbSet<Evento> Eventos { get; set; }

    }
}