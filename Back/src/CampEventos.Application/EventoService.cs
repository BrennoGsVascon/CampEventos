using System;
using System.Threading.Tasks;
using CampEventos.Persistence.Contratos;
using CampEventos.Application.Contratos;
using CampEventos.Domain;
using CampEventos.Application.Dtos;
using AutoMapper;
using CampEventos.Persistence.Models;
using System.Collections.Generic;


namespace CampEventos.Application
{
    public class EventoService : IEventoService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IEventoPersist _eventoPersist;
        private readonly IMapper mapper;
        public EventoService(IGeralPersist geralPersist, 
                             IEventoPersist eventoPersist, 
                             IMapper mapper)
                            
        {
            _geralPersist = geralPersist;
            _eventoPersist = eventoPersist;
            this.mapper = mapper;
        }
        public async Task<EventoDto> AddEventos(int userId, EventoDto model)
        {
            try
            {
                var evento = mapper.Map<Evento>(model);
                evento.UserId = userId;

                _geralPersist.Add<Evento>(evento);
                if(await _geralPersist.SaveChangesAsync())
                {
                    var eventoRetorno = await _eventoPersist.GetEventoByIdAsync(userId, evento.Id, false);
                    return mapper.Map<EventoDto>(eventoRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<EventoDto> UpdateEvento(int userId, int eventoId, EventoDto model)
        {
            try
            {
                var evento = await _eventoPersist.GetEventoByIdAsync(userId, eventoId, false);
                if (evento == null) return null;

                model.Id = evento.Id;
                model.UserId = userId;

                mapper.Map(model, evento);

                _geralPersist.Update<Evento>(evento);

                if(await _geralPersist.SaveChangesAsync())
                {
                    var eventoRetorno = await _eventoPersist.GetEventoByIdAsync(userId, evento.Id, false);
                    return mapper.Map<EventoDto>(eventoRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteEvento(int userId, int eventoId)
        {
            try
            {
                var evento = await _eventoPersist.GetEventoByIdAsync(userId, eventoId, false);
                if (evento == null) throw new Exception("Evento para delete não foi encontrado.");

                _geralPersist.Delete<Evento>(evento);
                return await _geralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<EventoDto>> GetAllEventosAsync(int userId, PageParams pageParams, bool includeApresentadores = false)
        {
            try
            {
                var eventos = await _eventoPersist.GetAllEventosAsync(
                    userId,
                    pageParams,
                    includeApresentadores
                );

                if (eventos == null) return null;

                var resultado = mapper.Map<List<EventoDto>>(eventos);

                return new PageList<EventoDto>(
                    resultado,
                    eventos.TotalCount,
                    eventos.CurrentPage,
                    eventos.PageSize
                );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<EventoDto> GetEventoByIdAsync(int userId, int eventoId, bool includeApresentadores = false)
        {
            try
            {
                var evento = await _eventoPersist.GetEventoByIdAsync(userId, eventoId, includeApresentadores);
                if (evento == null) return null;

                var resultado = mapper.Map<EventoDto>(evento);


                return resultado;
            }

            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

    }
}