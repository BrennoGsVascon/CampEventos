using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampEventos.Domain;
using CampEventos.Persistence.Models;

namespace CampEventos.Persistence.Contratos
{
    public interface IApresentadorPersist : IGeralPersist
    {
        
        Task<PageList<Apresentador>> GetAllApresentadoresAsync (PageParams pageParams, bool includeEventos = false);
        Task<Apresentador> GetApresentadorByUserIdAsync (int userId, bool includeEventos = false);
    }
}