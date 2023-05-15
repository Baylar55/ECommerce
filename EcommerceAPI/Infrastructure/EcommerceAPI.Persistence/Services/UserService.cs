using EcommerceAPI.Application.Abstractions.Services;
using EcommerceAPI.Application.DTOs.User;
using EcommerceAPI.Application.Exceptions;
using EcommerceAPI.Application.Features.Commands.AppUser.CreateUser;
using EcommerceAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserResponseDTO> CreateAsync(CreateUserDTO model)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                NameSurname = model.NameSurname,
                UserName = model.Username,
                Email = model.Email,
            }, model.Password);

            CreateUserResponseDTO response = new() { Succeeded = result.Succeeded };

            if (result.Succeeded)
                response.Message = "User is created successfully";
            else
                foreach (var error in result.Errors)
                    response.Message += $"{error.Code} - {error.Description}\n"; 

            return response;
        }

        public async Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate)
        {            
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = accessTokenDate.AddSeconds(addOnAccessTokenDate);
                await _userManager.UpdateAsync(user);
            }
            else
                throw new NotFoundUserException();
        }
    }
}
