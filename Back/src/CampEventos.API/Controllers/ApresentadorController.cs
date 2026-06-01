using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CampEventos.Application.Contratos;
using Microsoft.AspNetCore.Http;
using CampEventos.Application.Dtos;
using CampEventos.API.Extensions;
using Microsoft.AspNetCore.Authorization;
using CampEventos.Persistence.Models;

namespace CampEventos.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ApresentadorController : ControllerBase
    {
        private readonly IApresentadorService _apresentadorService;
        private readonly IAccountService _accountService;

        public ApresentadorController(
            IApresentadorService apresentadorService,
            IAccountService accountService
        )
        {
            _apresentadorService = apresentadorService;
            _accountService = accountService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll([FromQuery] PageParams pageParams)
        {
            try
            {
                var apresentadores = await _apresentadorService.GetAllApresentadoresAsync(pageParams, true);

                if (apresentadores == null) return NoContent();

                Response.AddPagination(apresentadores);

                return Ok(apresentadores);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar apresentadores. Erro: {ex.Message}"
                );
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetApresentador()
        {
            try
            {
                var apresentador = await _apresentadorService.GetApresentadorByUserIdAsync(
                    User.GetUserId(),
                    true
                );

                if (apresentador == null) return NoContent();

                return Ok(apresentador);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar apresentador. Erro: {ex.Message}"
                );
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(ApresentadorAddDto model)
        {
            try
            {
                var apresentador = await _apresentadorService.GetApresentadorByUserIdAsync(
                    User.GetUserId(),
                    false
                );

                if (apresentador == null)
                    apresentador = await _apresentadorService.AddApresentador(
                        User.GetUserId(),
                        model
                    );

                return Ok(apresentador);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar adicionar apresentador. Erro: {ex.Message}"
                );
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(ApresentadorUpdateDto model)
        {
            try
            {
                var apresentador = await _apresentadorService.UpdateApresentador(
                    User.GetUserId(),
                    model
                );

                if (apresentador == null) return NoContent();

                return Ok(apresentador);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar atualizar apresentador. Erro: {ex.Message}"
                );
            }
        }
    }
}