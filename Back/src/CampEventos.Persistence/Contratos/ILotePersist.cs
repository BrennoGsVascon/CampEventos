using System.Threading.Tasks;
using CampEventos.Domain;

namespace CampEventos.Persistence.Contratos
{
    public interface ILotePersist
    {
        Task<Lote[]> GetLotesByEventoIdAsync (int eventoId);
        Task<Lote> GetLoteByIdsAsync (int eventoId, int loteId);
    }
}