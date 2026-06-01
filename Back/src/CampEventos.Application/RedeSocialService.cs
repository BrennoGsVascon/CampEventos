using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CampEventos.Application.Contratos;
using CampEventos.Application.Dtos;
using CampEventos.Domain;
using CampEventos.Persistence;
using CampEventos.Persistence.Contratos;

namespace CampEventos.Application
{
    public class RedeSocialService : IRedeSocialService
    {
        private IRedeSocialPersist _redeSocialPersist;
        private IMapper _mapper;

        public RedeSocialService(IRedeSocialPersist  redeSocialPersist, IMapper mapper)
        {
            _redeSocialPersist = redeSocialPersist;
            _mapper = mapper;
        }
         public async Task AddRedeSocial( int id, RedeSocialDto model, bool isEvento)
        {
            var redeSocial = _mapper.Map<RedeSocial>(model);

            if (isEvento)
            {
                redeSocial.EventoId = id;
                redeSocial.ApresentadorId = null;
            }
            else
            {
                redeSocial.EventoId = null;
                redeSocial.ApresentadorId = id;
            }

            _redeSocialPersist.Add(redeSocial);
            await _redeSocialPersist.SaveChangesAsync();
        }

        public async Task<RedeSocialDto[]> SaveByEvento( int eventoId, RedeSocialDto[] models)
        {
            try
            {
                var redesSociais = await _redeSocialPersist.GetAllByEventoIdAsync(eventoId);

                foreach (var model in models)
                {
                    if (model.Id == 0)
                    {
                        await AddRedeSocial(eventoId, model, true);
                    }
                    else
                    {
                        var redeSocial = redesSociais.FirstOrDefault(rs => rs.Id == model.Id);

                        model.EventoId = eventoId;

                        _mapper.Map(model, redeSocial);
                        _redeSocialPersist.Update(redeSocial);

                        await _redeSocialPersist.SaveChangesAsync();
                    }
                }

                var retorno = await _redeSocialPersist.GetAllByEventoIdAsync(eventoId);

                return _mapper.Map<RedeSocialDto[]>(retorno);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RedeSocialDto[]> SaveByApresentador( int apresentadorId, RedeSocialDto[] models)
        {
            try
            {
                var redesSociais = await _redeSocialPersist.GetAllByApresentadorIdAsync(apresentadorId);

                foreach (var model in models)
                {
                    if (model.Id == 0)
                    {
                        await AddRedeSocial(apresentadorId, model, false);
                    }
                    else
                    {
                        var redeSocial = redesSociais.FirstOrDefault(rs => rs.Id == model.Id);

                        model.ApresentadorId = apresentadorId;

                        _mapper.Map(model, redeSocial);
                        _redeSocialPersist.Update(redeSocial);

                        await _redeSocialPersist.SaveChangesAsync();
                    }
                }

                var retorno = await _redeSocialPersist.GetAllByApresentadorIdAsync( apresentadorId);

                return _mapper.Map<RedeSocialDto[]>(retorno);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteByEvento( int eventoId, int redeSocialId)
        {
            try
            {
                var redeSocial = await _redeSocialPersist.GetRedeSocialEventoByIdsAsync( eventoId, redeSocialId);

                if (redeSocial == null)
                    throw new Exception("Rede social por evento não encontrada.");

                _redeSocialPersist.Delete(redeSocial);

                return await _redeSocialPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteByApresentador(int apresentadorId, int redeSocialId)
        {
            try
            {
                var redeSocial =
                    await _redeSocialPersist.GetRedeSocialApresentadorByIdsAsync( apresentadorId, redeSocialId);

                if (redeSocial == null)
                    throw new Exception("Rede social por apresentador não encontrada.");

                _redeSocialPersist.Delete(redeSocial);

                return await _redeSocialPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RedeSocialDto[]> GetAllByEventoIdAsync(int eventoId)
        {
            var redesSociais = await _redeSocialPersist.GetAllByEventoIdAsync(eventoId);

            return _mapper.Map<RedeSocialDto[]>(redesSociais);
        }

        public async Task<RedeSocialDto[]> GetAllByApresentadorIdAsync( int apresentadorId)
        {
            var redesSociais = await _redeSocialPersist.GetAllByApresentadorIdAsync(
                    apresentadorId
                );

            return _mapper.Map<RedeSocialDto[]>(redesSociais);
        }

        public async Task<RedeSocialDto> GetRedeSocialEventoByIdsAsync( int eventoId, int redeSocialId
        )
        {
            var redeSocial = await _redeSocialPersist.GetRedeSocialEventoByIdsAsync( eventoId, redeSocialId);

            return _mapper.Map<RedeSocialDto>(redeSocial);
        }

        public async Task<RedeSocialDto> GetRedeSocialApresentadorByIdsAsync( int apresentadorId, int redeSocialId)
        {
            var redeSocial = await _redeSocialPersist.GetRedeSocialApresentadorByIdsAsync( apresentadorId, redeSocialId);

            return _mapper.Map<RedeSocialDto>(redeSocial);
        }
    }
}