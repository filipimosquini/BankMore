using Account.Application.Account.Dto;
using MediatR;

namespace Account.Application.Account.Commands.CreateAccount;

public record CreateAccountCommand(string Holder, string UserId) : IRequest<CreateAccountDto>;