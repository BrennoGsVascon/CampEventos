using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CampEventos.Application.Contratos;
using CampEventos.Application.Dtos;
using CampEventos.Domain;
using CampEventos.Persistence.Contratos;
using CampEventos.Persistence.Models;

namespace CampEventos.Application
{
    public class ApresentadorService : IApresentadorService
    {
        private IApresentadorPersist _apresentadorPersist;
        private IMapper _mapper;

        public ApresentadorService(IApresentadorPersist apresentadorPersist, IMapper mapper)
        {
            _apresentadorPersist = apresentadorPersist;
            _mapper = mapper;
        }

        public async Task<ApresentadorDto> AddApresentador(int userId, ApresentadorAddDto model)
        {
            try
            {
                var apresentador = _mapper.Map<Apresentador>(model);
                apresentador.UserId = userId;

                _apresentadorPersist.Add(apresentador);

                if (await _apresentadorPersist.SaveChangesAsync())
                {
                    var apresentadorRetorno = await _apresentadorPersist.GetApresentadorByUserIdAsync( userId, false);

                    return _mapper.Map<ApresentadorDto>(apresentadorRetorno);
                }

                return null;
            }
            catch (Exception ex)

            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApresentadorDto> UpdateApresentador(int userId, ApresentadorUpdateDto model)
        {
             try
            {
                var apresentador = await _apresentadorPersist.GetApresentadorByUserIdAsync( userId, false);

                if (apresentador == null) return null;

                model.Id = apresentador.Id;
                model.UserId = userId;

                _mapper.Map(model, apresentador);

                _apresentadorPersist.Update(apresentador);

                if (await _apresentadorPersist.SaveChangesAsync())
                {
                    var apresentadorRetorno = await _apresentadorPersist.GetApresentadorByUserIdAsync( userId, false);

                    return _mapper.Map<ApresentadorDto>(apresentadorRetorno);
                }

                return null;
            }
            catch (Exception ex)

            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<PageList<ApresentadorDto>> GetAllApresentadoresAsync(PageParams pageParams, bool includeEventos = false)
        {
            try
            {
                var apresentadores = await _apresentadorPersist.GetAllApresentadoresAsync( pageParams, includeEventos);

                if (apresentadores == null) return null;

                var resultado = _mapper.Map<List<ApresentadorDto>>(apresentadores);

                return new PageList<ApresentadorDto>(
                    resultado,
                    apresentadores.TotalCount,
                    apresentadores.CurrentPage,
                    apresentadores.PageSize
                );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApresentadorDto> GetApresentadorByUserIdAsync(int userId, bool includeEventos = false)
        {
            try
            {
                var apresentador = await _apresentadorPersist.GetApresentadorByUserIdAsync( userId, includeEventos);

                if (apresentador == null) return null;

                return _mapper.Map<ApresentadorDto>(apresentador);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}