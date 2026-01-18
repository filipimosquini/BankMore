using System;
using MediatR;

namespace Account.Application.Account.Commands.DeactivateAccount;

public record DeactivateAccountCommand(string AccountId) : IRequest<Unit>;