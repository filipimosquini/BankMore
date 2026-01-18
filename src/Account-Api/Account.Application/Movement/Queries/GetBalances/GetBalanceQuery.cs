using Account.Application.Movement.Dto;
using MediatR;

namespace Account.Application.Movement.Queries.GetBalances;

public class GetBalanceQuery : IRequest<GetBalanceQueryDto>
{
    /// <summary>
    /// The bank account number
    /// </summary>
    /// <example>123</example>
    public int AccountNumber { get; set; }
}