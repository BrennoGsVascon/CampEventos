using System;
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

        public async Task<SignInResult> CheckUserPasswordAsync(UserUpdateDto userUpdateDto, string password)
        {
            try
            {
                if (userUpdateDto == null || string.IsNullOrWhiteSpace(userUpdateDto.UserName))
                    return SignInResult.Failed;

                var user = await GetUserIdentityByUserNameAsync(userUpdateDto.UserName);

                if (user == null)
                    return SignInResult.Failed;

                return await _signInManager.CheckPasswordSignInAsync(user, password, false);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar verificar password. Erro: {ex.Message}");
            }
        }

        public async Task<User> CreateAccountAsync(UserDto userDto)
        {
            try
            {
                if (userDto == null)
                    return null;

                userDto.UserName = userDto.UserName?.ToLower();
                userDto.Email = userDto.Email?.ToLower();

                var user = _mapper.Map<User>(userDto);
                user.UserName = userDto.UserName;
                user.Email = userDto.Email;

                var result = await _userManager.CreateAsync(user, userDto.Password);

                if (result.Succeeded)
                    return user;

                throw new Exception(string.Join(" | ", result.Errors.Select(e => e.Description)));
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar criar conta. Erro: {ex.Message}");
            }
        }

        public async Task<UserUpdateDto> GetUserByUserNameAsync(string userName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userName))
                    return null;

                var user = await _userPersist.GetUserByUserNameAsync(userName.ToLower());

                if (user == null)
                    return null;

                return _mapper.Map<UserUpdateDto>(user);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar buscar usuário. Erro: {ex.Message}");
            }
        }

        public async Task<User> GetUserIdentityByUserNameAsync(string userName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userName))
                    return null;

                var normalizedUserName = userName.ToLower();

                return await _userManager.Users
                    .SingleOrDefaultAsync(user => user.UserName == normalizedUserName);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar buscar usuário do Identity. Erro: {ex.Message}");
            }
        }

        public async Task<UserUpdateDto> UpdateAccount(UserUpdateDto userUpdateDto)
        {
            try
            {
                if (userUpdateDto == null || string.IsNullOrWhiteSpace(userUpdateDto.UserName))
                    return null;

                var user = await _userPersist.GetUserByUserNameAsync(userUpdateDto.UserName.ToLower());

                if (user == null)
                    return null;

                _mapper.Map(userUpdateDto, user);

                user.UserName = user.UserName?.ToLower();
                user.Email = user.Email?.ToLower();

                if (!string.IsNullOrWhiteSpace(userUpdateDto.Password))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var result = await _userManager.ResetPasswordAsync(user, token, userUpdateDto.Password);

                    if (!result.Succeeded)
                        throw new Exception(string.Join(" | ", result.Errors.Select(e => e.Description)));
                }

                _userPersist.Update(user);

                if (await _userPersist.SaveChangesAsync())
                {
                    var userRetorno = await _userPersist.GetUserByUserNameAsync(user.UserName);
                    return _mapper.Map<UserUpdateDto>(userRetorno);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar atualizar usuário. Erro: {ex.Message}");
            }
        }

        public async Task<bool> UserExists(string userName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userName))
                    return false;

                return await _userManager.FindByNameAsync(userName.ToLower()) != null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao verificar se o usuário existe. Erro: {ex.Message}");
            }
        }
    }
}
