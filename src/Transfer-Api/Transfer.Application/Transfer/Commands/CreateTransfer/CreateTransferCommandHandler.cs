using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Transfer.Application.Services;
using Transfer.Core.TransferAggregate.Repositories;
using Transfer.Infrastructure.CrossCutting.Exceptions;

namespace Transfer.Application.Transfer.Commands.CreateTransfer;

public class CreateTransferCommandHandler(ITransferService transferService, ITransferRepository transferRepository) : IRequestHandler<CreateTransferCommand, Unit>
{
    public async Task<Unit> Handle(CreateTransferCommand request, CancellationToken cancellationToken)
    {
        var sourceAccount = await transferService.VerifySourceAccount(Guid.Parse(request.UserId), cancellationToken);

        var destinationAccount = await transferService.VerifyDestinationAccount(request.DestinationAccountNumber, cancellationToken);

        if (sourceAccount.AccountNumber == destinationAccount.AccountNumber)
        {
            throw new SourceAndDestinationAccountAreEqualConflictException();
        }

        await transferService.CreateDebitMovementToSourceAccount(request.Amount, cancellationToken);

        await transferService.CreateCreditMovementToDestinationAccount(request.DestinationAccountNumber, request.Amount, cancellationToken);

        var transfer = new Core.TransferAggregate.Transfer(sourceAccount.AccountId, destinationAccount.AccountId,
            DateTime.Now, request.Amount);

        await transferRepository.AddAsync(transfer);

        await transferRepository.UnitOfWork.Commit(cancellationToken);

        return Unit.Value;
    }
}