using System.Threading.Tasks;
using CampEventos.Domain;

namespace CampEventos.Persistence.Contratos
{
    public interface IEventoPersist
    {
        Task<Evento[]> GetAllEventosByTemaAsync (string tema, bool includeApresentadores = false);
        Task<Evento[]> GetAllEventosAsync (bool includeApresentadores = false);
        Task<Evento> GetEventoByIdAsync (int eventoId, bool includeApresentadores = false);
    }
}