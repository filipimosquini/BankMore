using System;
using System.Threading.Tasks;

namespace Account.Application.Services;

public interface IMovementService
{
    Task<decimal> GetBalanceAsync(Guid accountId);
}