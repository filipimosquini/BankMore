using Authentication.Application.Services;
using Authentication.Application.Users.Commands.Dto;
using Authentication.Core.UserAggregate;
using Authentication.Infrastructure.CrossCutting.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Authentication.Application.Users.Commands.SignIn;

public class SignInCommandHandler(SignInManager<User> _signInManager, IAuthenticationService _authenticationService) 
    : IRequestHandler<SignInCommand, AuthenticationDto>
{
    public async Task<AuthenticationDto> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var result = await _signInManager.PasswordSignInAsync(request.Cpf, request.Password, false, true);

        if (result.IsLockedOut)
        {
            throw new BlockedAttemptInvalidTooManyRequestsException();
        }

        if (result.IsNotAllowed)
        {
            throw new BlockedNotAllowedLoginForbiddenException();
        }

        if (!result.Succeeded)
        {
            throw new BlockedNotSuccessLoginUnauthorizedException();
        }

        return await _authenticationService.GenerateJwtToken(request.Cpf);
    }
}