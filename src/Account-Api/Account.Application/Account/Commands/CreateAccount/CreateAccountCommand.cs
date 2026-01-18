using MediatR;

namespace Account.Application.Account.Commands.CreateAccount;

public record CreateAccountCommand(string Cpf, string UserId) : IRequest<Unit>;