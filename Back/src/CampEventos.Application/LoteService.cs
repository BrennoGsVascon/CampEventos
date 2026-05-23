using System;
using System.Threading.Tasks;
using CampEventos.Persistence.Contratos;
using CampEventos.Application.Contratos;
using CampEventos.Domain;
using CampEventos.Application.Dtos;
using AutoMapper;
using System.Linq;


namespace CampEventos.Application
{
    public class LoteService : ILoteService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly ILotePersist _lotePersist;
        private readonly IMapper mapper;
        public LoteService(IGeralPersist geralPersist, 
                           ILotePersist eventoPersist, 
                           IMapper mapper)
                            
        {
            _geralPersist = geralPersist;
            _lotePersist = eventoPersist;
            this.mapper = mapper;
        }
        public async Task AddLote(int eventoId, LoteDto model)
        {
            try
            {
                var lote = mapper.Map<Lote>(model);
                lote.EventoId = eventoId;

                _geralPersist.Add<Lote>(lote);

                await _geralPersist.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<LoteDto[]> SaveLotes(int eventoId, LoteDto[] models)
        {
            try
            {
                var lotes = await _lotePersist.GetLotesByEventoIdAsync(eventoId);
                if (lotes == null) return null;

                foreach (var model in models)
                {
                    if(model.Id == 0)
                    {
                        await AddLote(eventoId, model);
                    }
                    else
                    {
                        var lote = lotes.FirstOrDefault(lote => lote.Id == model.Id);
                        model.EventoId = eventoId;

                        mapper.Map(model, lote);

                        _geralPersist.Update<Lote>(lote);
                        
                        await _geralPersist.SaveChangesAsync();
                    }
                }

                    var loteRetorno = await _lotePersist.GetLotesByEventoIdAsync(eventoId);

                    return mapper.Map<LoteDto[]>(loteRetorno);

            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteLote(int eventoId, int loteId)
        {
            try
            {
                var lote = await _lotePersist.GetLoteByIdsAsync(eventoId, loteId);
                if (lote == null) throw new Exception("Lote para delete não foi encontrado.");

                _geralPersist.Delete<Lote>(lote);
                return await _geralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<LoteDto[]> GetLotesByEventoIdAsync(int eventoId)
        {
            try
            {
                var lotes = await _lotePersist.GetLotesByEventoIdAsync(eventoId);
                if (lotes == null) return null;

                var resultado = mapper.Map<LoteDto[]>(lotes);


                return resultado;
            }

            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<LoteDto> GetLoteByIdsAsync(int eventoId, int loteId)
        {
            try
            {
                var lote = await _lotePersist.GetLoteByIdsAsync(eventoId, loteId);
                if (lote == null) return null;

                var resultado = mapper.Map<LoteDto>(lote);


                return resultado;
            }

            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

    }
}