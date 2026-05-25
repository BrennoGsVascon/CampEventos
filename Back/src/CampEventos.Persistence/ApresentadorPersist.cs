using System;
using System.Linq;
using System.Threading.Tasks;
using CampEventos.Domain;
using CampEventos.Persistence.Contextos;
using CampEventos.Persistence.Contratos;
using Microsoft.EntityFrameworkCore;

namespace CampEventos.Persistence
{
    public class ApresentadorPersist : IApresentadorPersist
    {
        private readonly CampEventosContext _context;
        public ApresentadorPersist(CampEventosContext context)
        {
            _context = context;
        }

        public async Task<Apresentador[]> GetAllApresentadoresAsync(bool includeEventos)
        {
            IQueryable<Apresentador> query = _context.Apresentadores
                    .Include(ap => ap.RedesSociais);

            if (includeEventos)
            {
                query = query
                    .Include(ap => ap.ApresentadoresEventos)
                    .ThenInclude(ape => ape.Evento);
            }

                    query = query.AsNoTracking().OrderBy(ap =>ap.Id);

                    return await query.ToArrayAsync();
        }

        public async Task<Apresentador[]> GetAllApresentadoresByNomeAsync(string nome, bool includeEventos)
        {
            IQueryable<Apresentador> query = _context.Apresentadores
                    .Include(ap => ap.RedesSociais);

            if (includeEventos)
            {
                query = query
                    .Include(ap => ap.ApresentadoresEventos)
                    .ThenInclude(ape => ape.Apresentador);
            }

                    query = query.AsNoTracking().OrderBy(ap =>ap.Id)
                                 .Where(ap => ap.User.PrimeiroNome.ToLower().Contains(nome.ToLower()));

                    return await query.ToArrayAsync();
        }

         public async Task<Apresentador> GetApresentadorByIdAsync(int apresentadorId, bool includeEventos)
        {
           IQueryable<Apresentador> query = _context.Apresentadores
                    .Include(ap => ap.RedesSociais);

            if (includeEventos)
            {
                query = query
                    .Include(ap => ap.ApresentadoresEventos)
                    .ThenInclude(ape => ape.Apresentador);
            }

                    query = query.AsNoTracking().OrderBy(ap =>ap.Id)
                                 .Where(ap => ap.Id == apresentadorId);

                    return await query.FirstOrDefaultAsync();
        }
    }
}
