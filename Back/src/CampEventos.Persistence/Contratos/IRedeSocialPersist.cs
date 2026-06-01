using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampEventos.Domain;

namespace CampEventos.Persistence.Contratos
{
    public interface IRedeSocialPersist : IGeralPersist 
    {
        Task<RedeSocial> GetRedeSocialEventoByIdsAsync(int eventoId, int id);
        Task<RedeSocial> GetRedeSocialApresentadorByIdsAsync(int apresentadorId, int id);
        Task<RedeSocial[]> GetAllByEventoIdAsync(int eventoId);
        Task<RedeSocial[]> GetAllByApresentadorIdAsync(int apresentadorId);
    }
}