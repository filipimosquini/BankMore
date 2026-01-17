using Authentication.Application.Users.Queries.Dto;
using MediatR;

namespace Authentication.Application.Users.Queries.GetUserInformation;

public record GetUserInformationQuery(string UserId) : IRequest<UserInformationDto>;