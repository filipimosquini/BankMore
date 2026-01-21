using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Transfer.Application.Common.Integrations;
using Transfer.Infrastructure.Common.Integrations.AccountApi.Requests;
using Transfer.Infrastructure.Common.Integrations.AccountApi.Responses;
using Transfer.Infrastructure.CrossCutting.Exceptions;

namespace Transfer.Application.Services;

public class TransferService(IAccountApiClient accountApiClient) : ITransferService
{
    public async Task<InformationAccountResponse> VerifySourceAccount(Guid userId, CancellationToken cancellationToken)
    {
        var account = await accountApiClient.GetInformationAccountByHolderAsync(cancellationToken);

        if (account.StatusCode == HttpStatusCode.BadRequest)
        {
            throw new AccountInactiveException();
        }

        if (account.StatusCode == HttpStatusCode.NotFound || account.Content?.Data is null)
        {
            throw new AccountNotFoundException();
        }

        return account.Content.Data;
    }

    public async Task<InformationAccountResponse> VerifyDestinationAccount(int accountNumber, CancellationToken cancellationToken)
    {
        var account = await accountApiClient.GetInformationAccountAsync(new InformationAccountRequest(74867), cancellationToken);

        if (account.StatusCode == HttpStatusCode.BadRequest)
        {
            throw new AccountInactiveException();
        }

        if (account.StatusCode == HttpStatusCode.NotFound || account.Content?.Data is null)
        {
            throw new AccountNotFoundException();
        }

        return account.Content.Data;
    }

    public async Task CreateDebitMovementToSourceAccount(decimal amount, CancellationToken cancellationToken)
    {
        var movement = await accountApiClient.CreateMovementHolderAsync(Guid.NewGuid(), new MovementHolderRequest(amount, 'D'), cancellationToken);

        if (!movement.IsSuccessStatusCode)
        {
            throw new MovementDebitNotRegisteredUnprocessableEntityException();
        }
    }

    public async Task CreateCreditMovementToDestinationAccount(int? accountNumber, decimal amount, CancellationToken cancellationToken)
    {
        try
        {
            var movement = await accountApiClient.CreateMovementAsync(Guid.NewGuid(), new MovementRequest(accountNumber, amount, 'C'), cancellationToken);

            if (!movement.IsSuccessStatusCode)
            {
                throw new MovementCreditNotRegisteredUnprocessableEntityException();
            }
        }
        catch
        {
            await accountApiClient.CreateMovementHolderAsync(Guid.NewGuid(), new MovementHolderRequest(amount, 'C'), cancellationToken);
            throw;
        }
    }
}