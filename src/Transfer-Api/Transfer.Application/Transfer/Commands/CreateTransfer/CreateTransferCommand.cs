using MediatR;
using System;
using Transfer.Core.Common.Indepotencies;

namespace Transfer.Application.Transfer.Commands.CreateTransfer;

public record CreateTransferCommand(Guid RequestId) : IRequest<Unit>, IIdempotencyRequest;