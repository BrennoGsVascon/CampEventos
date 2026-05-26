using System.Threading.Tasks;
using CampEventos.Application.Dtos;

namespace CampEventos.Application.Contratos
{
    public interface IEventoService
    {
        Task<EventoDto> AddEventos(int userId, EventoDto model);
        Task<EventoDto> UpdateEvento(int userId, int eventoId, EventoDto model);
        Task<bool> DeleteEvento(int userId, int eventoId);

        Task<EventoDto[]> GetAllEventosAsync (int userId, bool includeApresentadores = false);
        Task<EventoDto[]> GetAllEventosByTemaAsync (int userId, string tema, bool includeApresentadores = false);
        Task<EventoDto> GetEventoByIdAsync (int userId, int eventoId, bool includeApresentadores = false);
    }
}