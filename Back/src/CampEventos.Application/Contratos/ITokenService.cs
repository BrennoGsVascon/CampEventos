using System.Threading.Tasks;
using CampEventos.Domain.Identity;

namespace CampEventos.Application.Contratos
{
    public interface ITokenService
    {
        Task<string> CreateToken(User user);
    }
}
