using System.Threading.Tasks;
using CampEventos.Domain;
using CampEventos.Persistence.Models;

namespace CampEventos.Persistence.Contratos
{
    public interface IEventoPersist
    {
        Task<PageList<Evento>> GetAllEventosAsync(
            int userId,
            PageParams pageParams,
            bool includeApresentadores = false
        );

        Task<Evento> GetEventoByIdAsync(
            int userId,
            int eventoId,
            bool includeApresentadores = false
        );
    }
}