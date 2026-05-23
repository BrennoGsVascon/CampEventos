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
    public class LotePersist : ILotePersist
    {
        private readonly CampEventosContext _context;

        public LotePersist(CampEventosContext context)
        {
            _context = context;
            
        }

        public async Task<Lote> GetLoteByIdsAsync(int eventoId, int loteId)
        {
            IQueryable<Lote> query = _context.Lotes;
            query = query.AsNoTracking()
                         .Where(lote => lote.EventoId == eventoId
                                                      && lote.Id == loteId);
                   return await query.FirstOrDefaultAsync();                                   
        }

        public async Task<Lote[]> GetLotesByEventoIdAsync(int eventoId)
        {
            IQueryable<Lote> query = _context.Lotes;
            query = query.AsNoTracking()
                         .Where(lote => lote.EventoId == eventoId);
                   return await query.ToArrayAsync();
        }
    }   
}