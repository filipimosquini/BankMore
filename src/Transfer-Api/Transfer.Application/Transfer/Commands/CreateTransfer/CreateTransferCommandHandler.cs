using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Transfer.Application.Transfer.Commands.CreateTransfer;

public class CreateTransferCommandHandler : IRequestHandler<CreateTransferCommand, Unit>
{
    public Task<Unit> Handle(CreateTransferCommand request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}