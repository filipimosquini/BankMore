using Account.Application.Account.Dto;
using MediatR;

namespace Account.Application.Account.Queries.GetAccountInformationByHolder;

public record GetAccountInformationByHolderQuery(string UserId) : IRequest<GetAccountInformationDto>;