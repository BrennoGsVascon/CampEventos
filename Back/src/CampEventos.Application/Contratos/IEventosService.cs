using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampEventos.Domain;

namespace CampEventos.Application.Contratos
{
    public interface IEventoService
    {
        Task<Evento> AddEventos(Evento model);
        Task<Evento> UpdateEvento(int eventoId, Evento model);
        Task<bool> DeleteEvento(int eventoId);

        Task<Evento[]> GetAllEventosAsync (bool includeApresentadores = false);
        Task<Evento[]> GetAllEventosByTemaAsync (string tema, bool includeApresentadores = false);
        Task<Evento> GetEventoByIdAsync (int eventoId, bool includeApresentadores = false);
    }
}