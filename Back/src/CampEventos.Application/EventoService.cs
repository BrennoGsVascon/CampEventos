using System;
using System.Threading.Tasks;
using CampEventos.Persistence.Contratos;
using CampEventos.Application.Contratos;
using CampEventos.Domain;
using CampEventos.Application.Dtos;
using AutoMapper;


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
        public async Task<EventoDto> AddEventos(EventoDto model)
        {
            try
            {
                var evento = mapper.Map<Evento>(model);

                _geralPersist.Add<Evento>(evento);
                if(await _geralPersist.SaveChangesAsync())
                {
                    var eventoRetorno = await _eventoPersist.GetEventoByIdAsync(evento.Id, false);
                    return mapper.Map<EventoDto>(eventoRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<EventoDto> UpdateEvento(int eventoId, EventoDto model)
        {
            try
            {
                var evento = await _eventoPersist.GetEventoByIdAsync(eventoId, false);
                if (evento == null) return null;

                model.Id = evento.Id;

                mapper.Map(model, evento);

                _geralPersist.Update<Evento>(evento);

                if(await _geralPersist.SaveChangesAsync())
                {
                    var eventoRetorno = await _eventoPersist.GetEventoByIdAsync(evento.Id, false);
                    return mapper.Map<EventoDto>(eventoRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteEvento(int eventoId)
        {
            try
            {
                var evento = await _eventoPersist.GetEventoByIdAsync(eventoId, false);
                if (evento == null) throw new Exception("Evento para delete não foi encontrado.");

                _geralPersist.Delete<Evento>(evento);
                return await _geralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto[]> GetAllEventosAsync(bool includeApresentadores = false)
        {
            try
            {
                var eventos = await _eventoPersist.GetAllEventosAsync(includeApresentadores);
                if (eventos == null) return null;

                var resultado = mapper.Map<EventoDto[]>(eventos);


                return resultado;
            }

            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto[]> GetAllEventosByTemaAsync(string tema, bool includeApresentadores = false)
        {
            try
            {
                var eventos = await _eventoPersist.GetAllEventosByTemaAsync(tema, includeApresentadores);
                if (eventos == null) return null;

                var resultado = mapper.Map<EventoDto[]>(eventos);


                return resultado;
            }

            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto> GetEventoByIdAsync(int eventoId, bool includeApresentadores = false)
        {
            try
            {
                var evento = await _eventoPersist.GetEventoByIdAsync(eventoId, includeApresentadores);
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