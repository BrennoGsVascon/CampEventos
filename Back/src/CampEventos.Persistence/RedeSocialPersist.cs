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
    public class RedeSocialPersist : GeralPersist, IRedeSocialPersist
    {

        private readonly CampEventosContext _context;
        public RedeSocialPersist(CampEventosContext context) : base(context)
        {
            _context = context;
        }

        public async Task<RedeSocial> GetRedeSocialEventoByIdsAsync(int eventoId, int id)
        {
            IQueryable<RedeSocial> query = _context.RedesSociais;

            query = query.AsNoTracking().Where(rs => rs.EventoId == eventoId && rs.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<RedeSocial> GetRedeSocialApresentadorByIdsAsync(int apresentadorId, int id)
        {
            IQueryable<RedeSocial> query = _context.RedesSociais;

            query = query.AsNoTracking().Where(rs => rs.ApresentadorId == apresentadorId && rs.Id == id);

            return await query.FirstOrDefaultAsync();
        }
        public async Task<RedeSocial[]> GetAllByEventoIdAsync(int eventoId)
        {
            IQueryable<RedeSocial> query = _context.RedesSociais;

            query = query.AsNoTracking().Where(rs => rs.EventoId == eventoId);

            return await query.ToArrayAsync();
        }

        public async Task<RedeSocial[]> GetAllByApresentadorIdAsync(int apresentadorId)
        {
            IQueryable<RedeSocial> query = _context.RedesSociais;

            query = query.AsNoTracking().Where(rs => rs.ApresentadorId == apresentadorId);

            return await query.ToArrayAsync();
        }

    }
}