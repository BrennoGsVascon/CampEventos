using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampEventos.Application.Dtos;
using CampEventos.Domain.Identity;

namespace CampEventos.Application.Contratos
{
    public interface ITokenService
    {
        Task<string> CreateToken(User user);
    }
}