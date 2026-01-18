using Account.Core.Common.Indepotencies;
using Account.Core.MovementAggregate.Enumerators;
using MediatR;
using System;

namespace Account.Application.Movement.Commands.CreateMovement;

public record CreateMovementCommand(Guid RequestId, int AccountNumber, decimal Amount, MovementTypeEnum MovementType, string UserId) : IRequest<Unit>, IIdempotencyRequest;
