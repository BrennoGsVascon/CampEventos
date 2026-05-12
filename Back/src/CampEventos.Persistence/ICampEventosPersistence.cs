using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampEventos.Domain;

namespace CampEventos.Persistence
{
    public interface ICampEventosPersistence
    {
        //Geral
        void Add<T>(T entity) where T: class;
        void Update<T>(T entity) where T: class;
        void Delete<T>(T entity) where T: class;
        void DeleteRange<T>(T entity) where T: class;

        Task<bool> SaveChangesAsync();

        //Eventos

        Task<Evento[]> GetAllEventosByTemaAsync (string tema, bool includeApresentadores);
        Task<Evento[]> GetAllEventosAsync (bool includeApresentadores);
        Task<Evento[]> GetEventoByIdAsync (int EventoId, bool includeApresentadores);

        //Apresentadores

        Task<Apresentador[]> GetAllApresentadoresByTemaAsync (string tema, bool includeEventos);
        Task<Apresentador[]> GetAllApresentadoressAsync (bool includeEventos);
        Task<Apresentador[]> GetApresentadorByIdAsync (int ApresentadorId, bool includeEventos);


        
    }
}