using Account.Application.Account.Dto;
using Account.Core.Common.Indepotencies;
using MediatR;
using System;

namespace Account.Application.Account.Commands.CreateAccount;

public record CreateAccountCommand(Guid RequestId, string Holder, string UserId) : IRequest<CreateAccountDto>, IIdempotencyRequest;