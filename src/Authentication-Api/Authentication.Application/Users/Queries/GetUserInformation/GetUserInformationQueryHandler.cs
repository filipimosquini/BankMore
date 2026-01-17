using Authentication.Application.Users.Queries.Dto;
using Authentication.Core.UserAggregate;
using Authentication.Infrastructure.CrossCutting.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Authentication.Application.Users.Queries.GetUserInformation;

public class GetUserInformationQueryHandler(UserManager<User> _userManager) : IRequestHandler<GetUserInformationQuery, UserInformationDto>
{
    public async Task<UserInformationDto> Handle(GetUserInformationQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);

        if (user is null)
        {
            throw new UserNotFoundException();
        }

        return new UserInformationDto(user?.Id, user?.Cpf);
    }
}