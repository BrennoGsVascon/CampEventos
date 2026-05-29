using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampEventos.Domain;
using CampEventos.Persistence.Contextos;
using CampEventos.Persistence.Contratos;
using CampEventos.Persistence.Models;
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
        public async Task<PageList<Evento>> GetAllEventosAsync(int userId,
         PageParams pageParams, 
         bool includeApresentadores = false)
         
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

            query = query
                .AsNoTracking()
                .Where(e => e.UserId == userId);

            if (!string.IsNullOrWhiteSpace(pageParams.Term))
            {
                query = query.Where(e =>
                    e.Tema.ToLower().Contains(pageParams.Term.ToLower()) ||
                    e.Local.ToLower().Contains(pageParams.Term.ToLower())
                );
            }

            query = query.OrderBy(e => e.Id);

            return await PageList<Evento>.CreateAsync(
                query,
                pageParams.PageNumber,
                pageParams.PageSize
            );
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