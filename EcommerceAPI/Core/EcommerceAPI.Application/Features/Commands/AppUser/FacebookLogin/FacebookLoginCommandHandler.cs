using EcommerceAPI.Application.Abstractions.Services;
using EcommerceAPI.Application.Abstractions.Token;
using EcommerceAPI.Application.DTOs;
using EcommerceAPI.Application.DTOs.Facebook;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace EcommerceAPI.Application.Features.Commands.AppUser.FacebookLogin
{
    public class FacebookLoginCommandHandler : IRequestHandler<FacebookLoginCommandRequest, FacebookLoginCommandResponse>
    {
        private readonly IAuthService _authService;

        public FacebookLoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<FacebookLoginCommandResponse> Handle(FacebookLoginCommandRequest request, CancellationToken cancellationToken)
        {
            var token = await _authService.FacebookLoginAsync(request.AuthToken, 900);

            return new()
            {
                Token = token,
            };
        }
    }
}
