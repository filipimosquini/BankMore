using Authentication.Application.Users.Commands.Dto;
using System.Threading.Tasks;

namespace Authentication.Application.Services;

public interface IAuthenticationService
{
    Task<AuthenticationDto> GenerateJwtToken(string cpf);
}