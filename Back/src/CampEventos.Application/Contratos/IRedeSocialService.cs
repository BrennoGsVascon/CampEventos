using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampEventos.Application.Dtos;

namespace CampEventos.Application.Contratos
{
    public interface IRedeSocialService
    {
        Task<RedeSocialDto[]> SaveByEvento( int eventoId, RedeSocialDto[] models);

        Task<RedeSocialDto[]> SaveByApresentador( int apresentadorId, RedeSocialDto[] models);

        Task<bool> DeleteByEvento( int eventoId, int redeSocialId);

        Task<bool> DeleteByApresentador( int apresentadorId, int redeSocialId);

        Task<RedeSocialDto[]> GetAllByEventoIdAsync(int eventoId);

        Task<RedeSocialDto[]> GetAllByApresentadorIdAsync(int apresentadorId);

        Task<RedeSocialDto> GetRedeSocialEventoByIdsAsync( int eventoId, int redeSocialId);

        Task<RedeSocialDto> GetRedeSocialApresentadorByIdsAsync( int apresentadorId, int redeSocialId);
    }
}