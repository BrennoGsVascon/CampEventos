using System;
using System.Linq;
using System.Threading.Tasks;
using CampEventos.Domain;
using CampEventos.Domain.Enum;
using CampEventos.Persistence.Contextos;
using CampEventos.Persistence.Contratos;
using CampEventos.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace CampEventos.Persistence
{
    public class ApresentadorPersist : GeralPersist, IApresentadorPersist
    {
        private readonly CampEventosContext _context;
        public ApresentadorPersist(CampEventosContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PageList<Apresentador>> GetAllApresentadoresAsync(PageParams pageParams, bool includeEventos = false)
        {
            IQueryable<Apresentador> query = _context.Apresentadores
                    .Include(ap => ap.User)
                    .Include(ap => ap.RedesSociais);

            if (includeEventos)
            {
                query = query
                    .Include(ap => ap.ApresentadoresEventos)
                    .ThenInclude(ape => ape.Evento);
            }

                    query = query.AsNoTracking()
                        .Where(ap => 
                        (
                            ap.MiniCurriculo.ToLower().Contains(pageParams.Term.ToLower()) ||
                            ap.User.PrimeiroNome.ToLower().Contains(pageParams.Term.ToLower()) ||
                            ap.User.UltimoNome.ToLower().Contains(pageParams.Term.ToLower())
                        )
                    && ap.User.Funcao == Funcao.Apresentador).OrderBy(ap => ap.Id);

                return await PageList<Apresentador>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
        }

        
         public async Task<Apresentador> GetApresentadorByUserIdAsync(int userId, bool includeEventos = false)
        {
           IQueryable<Apresentador> query = _context.Apresentadores
                    .Include(ap => ap.ApresentadoresEventos)
                    .Include(ap => ap.RedesSociais);

            if (includeEventos)
            {
                query = query
                    .Include(ap => ap.ApresentadoresEventos)
                    .ThenInclude(ape => ape.Evento);
            }

                    query = query.AsNoTracking().OrderBy(ap =>ap.Id)
                                 .Where(ap => ap.UserId == userId);

                    return await query.FirstOrDefaultAsync();
        }
    }
}
