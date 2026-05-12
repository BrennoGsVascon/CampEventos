using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampEventos.Domain;

namespace CampEventos.Persistence
{
    public class CampEventosPersistence : ICampEventosPersistence
    {
        public void Add<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public void DeleteRange<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<Apresentador[]> GetAllApresentadoresByTemaAsync(string tema, bool includeEventos)
        {
            throw new NotImplementedException();
        }

        public Task<Apresentador[]> GetAllApresentadoressAsync(bool includeEventos)
        {
            throw new NotImplementedException();
        }

        public Task<Evento[]> GetAllEventosAsync(bool includeApresentadores)
        {
            throw new NotImplementedException();
        }

        public Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includeApresentadores)
        {
            throw new NotImplementedException();
        }

        public Task<Apresentador[]> GetApresentadorByIdAsync(int ApresentadorId, bool includeEventos)
        {
            throw new NotImplementedException();
        }

        public Task<Evento[]> GetEventoByIdAsync(int EventoId, bool includeApresentadores)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void Update<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }
    }
}