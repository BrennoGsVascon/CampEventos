using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CampEventos.Application.Contratos;
using CampEventos.Application.Dtos;
using CampEventos.Domain.Identity;
using CampEventos.Persistence.Contratos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CampEventos.Application
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IUserPersist _userPersist;

        public AccountService(UserManager<User> userManager,
                             SignInManager<User> signInManager,
                             IMapper mapper,
                             IUserPersist userPersist)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _userPersist = userPersist;
        }
        
        public async Task<bool> UserExists(string userName)
        {
            try
            {
                return await _userManager.Users.AnyAsync(user => user.UserName == userName.ToLower());
            }
            catch (System.Exception ex)
            {
                
                throw new Exception($"Erro ao verificar se o Usuario existe . Erro :{ex.Message}");
            }
        }
        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            try
            {
                return await _userManager.Users
                    .SingleOrDefaultAsync(user =>
                        user.UserName == userName.ToLower());
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar buscar usuário. Erro: {ex.Message}");
            }
        }

        public async Task<UserUpdateDto> GetUserDtoByUserNameAsync(string userName)
        {
            try
            {
                var user = await _userPersist.GetUserByUserNameAsync(userName);
                if (user == null) return null;

                return _mapper.Map<UserUpdateDto>(user);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar buscar Usuario pelo UserName. Erro: {ex.Message}");
            }
        }

        public async Task<SignInResult> CheckUserPasswordAsync(User user, string password)
        {
            try
            {
                return await _signInManager.CheckPasswordSignInAsync(user, password, false);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar verificar password. Erro: {ex.Message}");
            }
        }

        public async Task<UserDto> CreateAccountAsync(UserDto userDto)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);
                var result = await _userManager.CreateAsync(user, userDto.Password);

                if (result.Succeeded)
                {
                    var userToReturn = _mapper.Map<UserDto>(user);
                    return userToReturn;
                }

                return null;
            }
            catch (System.Exception ex)
            {
                
                throw new Exception($"Erro ao tentar criar conta. Erro :{ex.Message}");
            }
        }

        public async Task<UserUpdateDto> GetUserbyUserNameAsync(string userName)
        {
            try
            {
                var user = await _userPersist.GetUserByUserNameAsync(userName);
                if(user == null) return null;

                var userUpdateDto = _mapper.Map<UserUpdateDto>(user);
                return userUpdateDto;
            }
            catch (System.Exception ex)
            {
                
                throw new Exception($"Erro ao tentar buscar Usuario pelo UserName. Erro :{ex.Message}");
            }
        }

        public async Task<UserUpdateDto> UpdateAccount(UserUpdateDto userUpdateDto)
        {
            try
            {
                var user = await _userPersist.GetUserByUserNameAsync(userUpdateDto.UserName);
                if(user == null) return null;

                _mapper.Map(userUpdateDto, user);

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, userUpdateDto.Password);

                _userPersist.Update<User>(user);

                if(await _userPersist.SaveChangesAsync())
                {
                    var userRetorno = await _userPersist.GetUserByUserNameAsync(user.UserName);

                    return _mapper.Map<UserUpdateDto>(userRetorno);
                }

                return null;
            }
            catch (System.Exception ex)
            {
                
                throw new Exception($"Erro ao tentar atualizar Usuario. Erro :{ex.Message}");
            }
        }


    }
}