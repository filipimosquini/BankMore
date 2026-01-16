using Authentication.Application.Services;
using Authentication.Application.Users.Commands.Dto;
using Authentication.Core.UserAggregate;
using Authentication.Infrastructure.CrossCutting.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Authentication.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler(SignInManager<User> _signInManager, UserManager<User> _userManager, IAuthenticationService _authenticationService) 
    : IRequestHandler<CreateUserCommand, AuthenticationDto>
{
    public async Task<AuthenticationDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            UserName = request.Cpf,
            Cpf = request.Cpf,
            EmailConfirmed = false
        };

        var createdUser = await _userManager.CreateAsync(user, request.Password);

        if (!createdUser.Succeeded)
        {
            throw new CreateUserBadRequestException(createdUser.Errors);
        }

        await _signInManager.SignInAsync(user, false);

        return await _authenticationService.GenerateJwtToken(request.Cpf);
    }
}