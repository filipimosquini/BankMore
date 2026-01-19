using Account.Core.Common.Indepotencies;
using Account.Core.MovementAggregate.Enumerators;
using MediatR;
using System;

namespace Account.Application.Movement.Commands.CreateMovementByHolder;

public record CreateMovementByHolderCommand(Guid RequestId, decimal Amount, MovementTypeEnum MovementType, string UserId) : IRequest<Unit>, IIdempotencyRequest;
