using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampEventos.Application.Dtos;
using CampEventos.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace CampEventos.Application.Contratos
{
    public interface IAccountService
    {
        Task<bool> UserExists(string userName);
        Task<User> GetUserByUserNameAsync(string userName);
        Task<UserUpdateDto> GetUserDtoByUserNameAsync(string userName);
        Task<SignInResult> CheckUserPasswordAsync(User user, string password);
        Task<UserDto> CreateAccountAsync(UserDto userDto);
        Task<UserUpdateDto> UpdateAccount(UserUpdateDto userUpdateDto);
    }
}