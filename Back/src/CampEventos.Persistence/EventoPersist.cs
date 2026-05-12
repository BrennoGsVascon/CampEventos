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
        }
        public async Task<Evento[]> GetAllEventosAsync(bool includeApresentadores = false)
        {
            IQueryable<Evento> query = _context.Eventos
                    .Include(e => e.Lotes)
                    .Include(e => e.RedeSociais);

            if (includeApresentadores)
            {
                query = query
                    .Include(e => e.ApresentadoresEventos)
                    .ThenInclude(ape => ape.Apresentador);
            }

                    query = query.OrderBy(e =>e.Id);

                    return await query.ToArrayAsync();
        }           

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includeEventos)
        {
            IQueryable<Evento> query = _context.Eventos
                    .Include(e => e.Lotes)
                    .Include(e => e.RedeSociais);

            if (includeEventos)
            {
                query = query
                    .Include(e => e.ApresentadoresEventos)
                    .ThenInclude(ape => ape.Apresentador);
            }

                    query = query.OrderBy(e =>e.Id)
                                 .Where(e => e.Tema.ToLower()
                                 .Contains(tema.ToLower()));

                    return await query.ToArrayAsync();
            
        }
        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includeApresentadores)
        {
            IQueryable<Evento> query = _context.Eventos
                    .Include(e => e.Lotes)
                    .Include(e => e.RedeSociais);

            if (includeApresentadores)
            {
                query = query
                    .Include(e => e.ApresentadoresEventos)
                    .ThenInclude(ape => ape.Apresentador);
            }

                    query = query.OrderBy(e =>e.Id)
                                 .Where(e => e.Id == eventoId);

                    return await query.FirstOrDefaultAsync();
        }
    }
}