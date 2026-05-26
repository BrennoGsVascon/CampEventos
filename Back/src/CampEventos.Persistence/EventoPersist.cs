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
    public class EventoPersist : IEventoPersist
    {
        private readonly CampEventosContext _context;

        public EventoPersist(CampEventosContext context)
        {
            _context = context;
            //_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public async Task<Evento[]> GetAllEventosAsync(int userId, bool includeApresentadores = false)
        {
            IQueryable<Evento> query = _context.Eventos
                    .Include(e => e.Lotes)
                    .Include(e => e.RedesSociais);

            if (includeApresentadores)
            {
                query = query
                    .Include(e => e.ApresentadoresEventos)
                    .ThenInclude(ape => ape.Apresentador);
            }

                    query = query.AsNoTracking()
                                 .Where(e => e.UserId == userId)
                                 .OrderBy(e =>e.Id);

                    return await query.ToArrayAsync();
        }           

        public async Task<Evento[]> GetAllEventosByTemaAsync(int userId, string tema, bool includeEventos)
        {
            IQueryable<Evento> query = _context.Eventos
                    .Include(e => e.Lotes)
                    .Include(e => e.RedesSociais);

            if (includeEventos)
            {
                query = query
                    .Include(e => e.ApresentadoresEventos)
                    .ThenInclude(ape => ape.Apresentador);
            }

                    query = query.AsNoTracking().OrderBy(e =>e.Id)
                                 .Where(e => e.Tema.ToLower().Contains(tema.ToLower()) && 
                                 e.UserId == userId);

                    return await query.ToArrayAsync();
            
        }
        public async Task<Evento> GetEventoByIdAsync(int userId, int eventoId, bool includeApresentadores)
        {
            IQueryable<Evento> query = _context.Eventos
                    .Include(e => e.Lotes)
                    .Include(e => e.RedesSociais);

            if (includeApresentadores)
            {
                query = query
                    .Include(e => e.ApresentadoresEventos)
                    .ThenInclude(ape => ape.Apresentador);
            }

                    query = query.AsNoTracking().OrderBy(e =>e.Id)
                                 .Where(e => e.Id == eventoId && e.UserId == userId);

                    return await query.FirstOrDefaultAsync();
        }
    }
}