using System.Threading.Tasks;
using CampEventos.Domain;

namespace CampEventos.Persistence.Contratos
{
    public interface IEventoPersist
    {
        Task<Evento[]> GetAllEventosByTemaAsync (int userId, string tema, bool includeApresentadores = false);
        Task<Evento[]> GetAllEventosAsync (int userId, bool includeApresentadores = false);
        Task<Evento> GetEventoByIdAsync (int userId, int eventoId, bool includeApresentadores = false);
    }
}