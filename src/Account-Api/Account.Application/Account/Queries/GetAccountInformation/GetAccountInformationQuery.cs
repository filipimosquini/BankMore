using Account.Application.Account.Dto;
using MediatR;

namespace Account.Application.Account.Queries.GetAccountInformation;

public record GetAccountInformationQuery(int AccountNumber) : IRequest<GetAccountInformationDto>;