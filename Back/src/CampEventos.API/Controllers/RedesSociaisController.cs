using System;
using System.Threading.Tasks;
using CampEventos.API.Extensions;
using CampEventos.Application.Contratos;
using CampEventos.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CampEventos.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RedesSociaisController : ControllerBase
    {
        private readonly IRedeSocialService _redeSocialService;
        private readonly IEventoService _eventoService;
        private readonly IApresentadorService _apresentadorService;

        public RedesSociaisController(
            IRedeSocialService redeSocialService,
            IEventoService eventoService,
            IApresentadorService apresentadorService)
        {
            _redeSocialService = redeSocialService;
            _eventoService = eventoService;
            _apresentadorService = apresentadorService;
        }

        [HttpGet("evento/{eventoId}")]
        public async Task<IActionResult> GetByEvento(int eventoId)
        {
            try
            {
                if (!(await AutorEvento(eventoId))) return Unauthorized();

                var redesSociais = await _redeSocialService.GetAllByEventoIdAsync(eventoId);

                if (redesSociais == null) return NoContent();

                return Ok(redesSociais);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar rede social por evento. Erro: {ex.Message}"
                );
            }
        }

        [HttpGet("apresentador")]
        public async Task<IActionResult> GetByApresentador()
        {
            try
            {
                var apresentador = await _apresentadorService.GetApresentadorByUserIdAsync(User.GetUserId());

                if (apresentador == null) return Unauthorized();

                var redesSociais = await _redeSocialService.GetAllByApresentadorIdAsync(apresentador.Id);

                if (redesSociais == null) return NoContent();

                return Ok(redesSociais);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar rede social por apresentador. Erro: {ex.Message}"
                );
            }
        }

        [HttpPut("evento/{eventoId}")]
        public async Task<IActionResult> SaveByEvento(int eventoId, RedeSocialDto[] models)
        {
            try
            {
                if (!(await AutorEvento(eventoId))) return Unauthorized();

                var redesSociais = await _redeSocialService.SaveByEvento(eventoId, models);

                if (redesSociais == null) return NoContent();

                return Ok(redesSociais);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar salvar rede social por evento. Erro: {ex.Message}"
                );
            }
        }

        [HttpPut("apresentador")]
        public async Task<IActionResult> SaveByApresentador(RedeSocialDto[] models)
        {
            try
            {
                var apresentador = await _apresentadorService.GetApresentadorByUserIdAsync(User.GetUserId());

                if (apresentador == null) return Unauthorized();

                var redesSociais = await _redeSocialService.SaveByApresentador(apresentador.Id, models);

                if (redesSociais == null) return NoContent();

                return Ok(redesSociais);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar salvar rede social por apresentador. Erro: {ex.Message}"
                );
            }
        }

        [HttpDelete("evento/{eventoId}/{redeSocialId}")]
        public async Task<IActionResult> DeleteByEvento(int eventoId, int redeSocialId)
        {
            try
            {
                if (!(await AutorEvento(eventoId))) return Unauthorized();

                var redeSocial = await _redeSocialService.GetRedeSocialEventoByIdsAsync(eventoId, redeSocialId);

                if (redeSocial == null) return NoContent();

                return await _redeSocialService.DeleteByEvento(eventoId, redeSocialId)
                    ? Ok(new { message = "Rede social deletada" })
                    : throw new Exception("Erro ao deletar rede social por evento.");
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar deletar rede social por evento. Erro: {ex.Message}"
                );
            }
        }

        [HttpDelete("apresentador/{redeSocialId}")]
        public async Task<IActionResult> DeleteByApresentador(int redeSocialId)
        {
            try
            {
                var apresentador = await _apresentadorService.GetApresentadorByUserIdAsync(User.GetUserId());

                if (apresentador == null) return Unauthorized();

                var redeSocial = await _redeSocialService.GetRedeSocialApresentadorByIdsAsync(
                    apresentador.Id,
                    redeSocialId
                );

                if (redeSocial == null) return NoContent();

                return await _redeSocialService.DeleteByApresentador(apresentador.Id, redeSocialId)
                    ? Ok(new { message = "Rede social deletada" })
                    : throw new Exception("Erro ao deletar rede social por apresentador.");
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar deletar rede social por apresentador. Erro: {ex.Message}"
                );
            }
        }

        [NonAction]
        private async Task<bool> AutorEvento(int eventoId)
        {
            var evento = await _eventoService.GetEventoByIdAsync(
                User.GetUserId(),
                eventoId,
                false
            );

            return evento != null;
        }
    }
}