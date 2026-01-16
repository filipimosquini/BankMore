using Authentication.Api.Documentation.Swagger.Examples;
using Authentication.Application.Users.Commands.CreateUser;
using Authentication.Application.Users.Commands.Dto;
using Authentication.Application.Users.Commands.SignIn;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Filters;
using System.Net;
using System.Threading.Tasks;

namespace Authentication.Api.Controllers;

[Route("api/authentication")]
[ApiController]
public class AuthenticationController : BaseController<AuthenticationController>
{
    public AuthenticationController(ILoggerFactory loggerFactory, IMediator mediatorService) 
        : base(loggerFactory, mediatorService)
    {
    }

    /// <summary>
    /// api/authentication/signin.
    /// </summary>
    /// <remarks>
    /// <p>
    /// <b> Description: </b><br />
    /// This method make login and returns JWT token. <br />
    /// </p>
    /// <p>
    /// <b> Requirements: </b><br />
    /// Not Exists. <br />
    /// </p>
    /// </remarks>
    /// <response code="200">OK</response>
    /// <response code="400"> Bad Request
    /// <ul>
    ///     <li>Cpf.IsInvalid</li>
    ///     <li>Cpf.IsRequired</li>
    ///     <li>Cpf.MustBeInformed</li>
    ///     <li>Password.InvalidLength</li>
    ///     <li>Password.IsRequired</li>
    ///     <li>Password.MustBeInformed</li>
    /// </ul>
    /// </response> 
    /// <response code="401"> Unauthorized
    /// <ul>
    ///     <li>Blocked.UserUnauthorized</li>
    /// </ul>
    /// </response> 
    /// <response code="403"> Forbidden
    /// <ul>
    ///     <li>Blocked.NotAllowedLogin</li>
    /// </ul>
    /// </response>
    /// <response code="429"> Too many requests
    /// <ul>
    ///     <li>Blocked.InvalidAttempt</li>
    /// </ul>
    /// </response>
    /// <response code="500"> InternalServerError
    /// <ul>
    ///     <li>Error.Unexpected</li>
    /// </ul>
    /// </response>
    [HttpPost("signin")]
    [SwaggerRequestExample(typeof(SignInCommand), typeof(SignInCommandExample))]
    [ProducesResponseType(typeof(AuthenticationDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> SignInAsync([FromBody] SignInCommand command)
        => await ExecuteAsync(async () => await _mediatorService.Send(command));

    /// <summary>
    /// api/authentication/users/create.
    /// </summary>
    /// <remarks>
    /// <p>
    /// <b> Description: </b><br />
    /// This method create a user. <br />
    /// </p>
    /// <p>
    /// <b> Requirements: </b><br />
    /// Not Exists. <br />
    /// </p>
    /// </remarks>
    /// <response code="201">Created</response>
    /// <response code="400"> Bad Request
    /// <ul>
    ///     <li>Cpf.IsInvalid</li>
    ///     <li>Cpf.IsRequired</li>
    ///     <li>Cpf.MustBeInformed</li>
    ///     <li>Password.InvalidLength</li>
    ///     <li>Password.IsRequired</li>
    ///     <li>Password.MustBeInformed</li>
    ///     <li>ConfirmPassword.InvalidLength</li>
    ///     <li>ConfirmPassword.IsRequired</li>
    ///     <li>ConfirmPassword.MustBeInformed</li>
    /// </ul>
    /// </response> 
    /// <response code="500">InternalServerError
    /// <ul>
    ///     <li>Error.Unexpected</li>
    /// </ul>
    /// </response>
    [HttpPost("users/create")]
    [SwaggerRequestExample(typeof(CreateUserCommand), typeof(CreateUserCommandExample))]
    [ProducesResponseType(typeof(AuthenticationDto), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserCommand command)
        => await ExecuteAsync(async () => await _mediatorService.Send(command), HttpStatusCode.Created);
}