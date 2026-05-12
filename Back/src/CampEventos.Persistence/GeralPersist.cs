using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampEventos.Domain;
using CampEventos.Persistence.Contextos;
using CampEventos.Persistence.Contratos;
using Microsoft.EntityFrameworkCore;


namespace CampEventos.Persistence
{
    public class GeralPersist : IGeralPersist
    {
        private readonly CampEventosContext _context;

        public GeralPersist(CampEventosContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void DeleteRange<T>(T entity) where T : class
        {
            _context.RemoveRange(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return(await _context.SaveChangesAsync()) > 0;
        }
    }
}