using EcommerceAPI.Application.Abstractions.Services;
using EcommerceAPI.Application.DTOs.User;
using MediatR;

namespace EcommerceAPI.Application.Features.Commands.AppUser.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        private readonly IUserService _userService;

        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {

            CreateUserResponseDTO response = await _userService.CreateAsync(new()
            {
                Email = request.Email,
                NameSurname = request.NameSurname,
                Password = request.Password,
                RepeatPassword = request.RepeatPassword,
                Username = request.Username,
            });

            return new()
            {
                IsSucceeded = response.Succeeded,
                Message = response.Message,
            };

        }
    }
}
