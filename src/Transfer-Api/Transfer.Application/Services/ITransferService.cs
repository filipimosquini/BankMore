using System;
using System.Threading;
using System.Threading.Tasks;
using Transfer.Infrastructure.Common.Integrations.AccountApi.Responses;

namespace Transfer.Application.Services;

public interface ITransferService
{
    Task CreateDebitMovementToSourceAccount(decimal amount, CancellationToken cancellationToken);
    Task CreateCreditMovementToDestinationAccount(int? accountNumber, decimal amount, CancellationToken cancellationToken);
    Task<InformationAccountResponse> VerifySourceAccount(Guid userId, CancellationToken cancellationToken);
    Task<InformationAccountResponse> VerifyDestinationAccount(int accountNumber, CancellationToken cancellationToken);
}