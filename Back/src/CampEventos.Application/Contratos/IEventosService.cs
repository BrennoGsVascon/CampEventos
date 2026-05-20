using System.Threading.Tasks;
using CampEventos.Application.Dtos;

namespace CampEventos.Application.Contratos
{
    public interface IEventoService
    {
        Task<EventoDto> AddEventos(EventoDto model);
        Task<EventoDto> UpdateEvento(int eventoId, EventoDto model);
        Task<bool> DeleteEvento(int eventoId);

        Task<EventoDto[]> GetAllEventosAsync (bool includeApresentadores = false);
        Task<EventoDto[]> GetAllEventosByTemaAsync (string tema, bool includeApresentadores = false);
        Task<EventoDto> GetEventoByIdAsync (int eventoId, bool includeApresentadores = false);
    }
}