using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CampEventos.Application.Contratos;
using CampEventos.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CampEventos.Domain.Identity;

namespace CampEventos.API.Controllers
{
    
    [Authorize]
    [ApiController]
    [Route("[controller]")]
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

        [HttpGet("GetUser/{userName}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUser(string userName)
        {
        
        try
        {
            var user = await _accountService.GetUserDtoByUserNameAsync(userName);
            return Ok(user);
        }
        catch (System.Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError,
                                    $"Erro ao tentar recuperar Usuario. Erro{ex.Message}");
        }
        
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserDto userDto)
        {
        
        try
        {
            if(await _accountService.UserExists(userDto.UserName))
                return BadRequest("Usuario ja existe!");

            var user = await _accountService.CreateAccountAsync(userDto);
            if(user != null)
                return Ok(user);

            return BadRequest("Usuario nao criado, tente novamente mais tarde!");

        }
        catch (System.Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError,
                                    $"Erro ao tentar recuperar Usuario. Erro{ex.Message}");
        }
        
        }
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto userLogin)
        {
        
        try
        {
            var user = await _accountService.GetUserByUserNameAsync(userLogin.UserName);
            if(user == null) return Unauthorized("Usuario ou senha está errado");
            
            var result = await _accountService.CheckUserPasswordAsync(user,userLogin.Password);
            if(!result.Succeeded) return Unauthorized();

            return Ok(new
            {
                userName = user.UserName,
                PrimeiroNome = user.PrimeiroNome,
                token =  _tokenService.CreateToken(user).Result
            });

            

        }
        catch (System.Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError,
                                    $"Erro ao tentar recuperar Usuario. Erro{ex.Message}");
        }
        
        }
    }    
}
