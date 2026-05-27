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
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;

        public AccountController(IAccountService accountService,
                                 ITokenService tokenService)
        {
            _accountService = accountService;
            _tokenService = tokenService;
        }

        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                var userName = User.GetUserName();

                if (string.IsNullOrWhiteSpace(userName))
                    return Unauthorized("Usuário inválido.");

                var user = await _accountService.GetUserByUserNameAsync(userName);

                if (user == null)
                    return Unauthorized("Usuário inválido.");

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar usuário. Erro: {ex.Message}");
            }
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            try
            {
                if (userDto == null)
                    return BadRequest("Dados de usuário inválidos.");

                if (await _accountService.UserExists(userDto.UserName))
                    return BadRequest("Usuário já existe!");

                var user = await _accountService.CreateAccountAsync(userDto);

                if (user == null)
                    return BadRequest("Usuário não criado, tente novamente mais tarde!");

                return Ok(new
                {
                    userName = user.UserName,
                    email = user.Email,
                    primeiroNome = user.PrimeiroNome,
                    ultimoNome = user.UltimoNome,
                    token = await _tokenService.CreateToken(user)
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar registrar usuário. Erro: {ex.Message}");
            }
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto userLogin)
        {
            try
            {
                if (userLogin == null)
                    return BadRequest("Dados de login inválidos.");

                var userDto = await _accountService.GetUserByUserNameAsync(userLogin.UserName);

                if (userDto == null)
                    return Unauthorized("Usuário ou senha está errado.");

                var result = await _accountService.CheckUserPasswordAsync(userDto, userLogin.Password);

                if (!result.Succeeded)
                    return Unauthorized("Usuário ou senha está errado.");

                var user = await _accountService.GetUserIdentityByUserNameAsync(userDto.UserName);

                if (user == null)
                    return Unauthorized("Usuário ou senha está errado.");

                return Ok(new
                {
                    userName = user.UserName,
                    email = user.Email,
                    primeiroNome = user.PrimeiroNome,
                    ultimoNome = user.UltimoNome,
                    token = await _tokenService.CreateToken(user)
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar realizar login. Erro: {ex.Message}");
            }
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UserUpdateDto userUpdateDto)
        {
            try
            {
                if (userUpdateDto == null)
                    return BadRequest("Dados de usuário inválidos.");

                userUpdateDto.UserName = User.GetUserName();

                if (string.IsNullOrWhiteSpace(userUpdateDto.UserName))
                    return Unauthorized("Usuário inválido.");

                var userReturn = await _accountService.UpdateAccount(userUpdateDto);

                if (userReturn == null)
                    return NoContent();

                var user = await _accountService.GetUserIdentityByUserNameAsync(userReturn.UserName);

                if (user == null)
                    return Unauthorized("Usuário inválido.");

                return Ok(new
                {
                    userName = userReturn.UserName,
                    email = userReturn.Email,
                    primeiroNome = userReturn.PrimeiroNome,
                    ultimoNome = userReturn.UltimoNome,
                    phoneNumber = userReturn.PhoneNumber,
                    imagemURL = userReturn.ImagemURL,
                    token = await _tokenService.CreateToken(user)
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar atualizar usuário. Erro: {ex.Message}");
            }
        }
    }
}
