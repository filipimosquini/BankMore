using Account.Application.Movement.Dto;
using MediatR;

namespace Account.Application.Movement.Queries.GetBalances;

public record GetBalanceQuery(int AccountNumber) : IRequest<GetBalanceQueryDto>;