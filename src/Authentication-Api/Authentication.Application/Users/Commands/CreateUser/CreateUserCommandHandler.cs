using Authentication.Application.Users.Commands.Dto;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Authentication.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler() 
    : IRequestHandler<CreateUserCommand, AuthenticationDto>
{
    public Task<AuthenticationDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}