using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampEventos.Domain;

namespace CampEventos.Persistence.Contratos
{
    public interface IApresentadorPersist
    {
        //Apresentadores

        Task<Apresentador[]> GetAllApresentadoresAsync (bool includeEventos);
        Task<Apresentador[]> GetAllApresentadoresByNomeAsync (string nome, bool includeEventos);
        Task<Apresentador> GetApresentadorByIdAsync (int apresentadorId, bool includeEventos);
    }
}