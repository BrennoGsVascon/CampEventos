using System.Threading.Tasks;
using CampEventos.Application.Dtos;
using CampEventos.Persistence.Models;

namespace CampEventos.Application.Contratos
{
    public interface IApresentadorService
    {
        Task<ApresentadorDto> AddApresentador(int userId, ApresentadorAddDto model);
        Task<ApresentadorDto> UpdateApresentador(int userId, ApresentadorUpdateDto model);
        Task<PageList<ApresentadorDto>> GetAllApresentadoresAsync(PageParams pageParams, bool includeEventos = false);
        Task<ApresentadorDto> GetApresentadorByUserIdAsync(int userId, bool includeEventos = false);
    }
}