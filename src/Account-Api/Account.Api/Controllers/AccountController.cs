using Account.Api.Controllers.Requests;
using Account.Api.Documentation.Swagger.Examples;
using Account.Application.Account.Commands.CreateAccount;
using Account.Application.Account.Commands.DeactivateAccount;
using Account.Application.Account.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Account.Api.Controllers;

[Route("api/accounts")]
[ApiController]
public class AccountController(ILoggerFactory loggerFactory, IMediator mediatorService) : BaseController<AccountController>(loggerFactory, mediatorService)
{
    /// <summary>
    /// api/accounts.
    /// </summary>
    /// <remarks>
    /// <p>
    /// <b> Description: </b><br />
    /// This method create an account. <br />
    /// </p>
    /// <p>
    /// <b> Requirements: </b><br />
    /// Is required to be authenticated. <br />
    /// </p>
    /// </remarks>
    /// <response code="201">Created</response>
    /// <response code="400"> Bad Request
    /// <ul>
    ///     <li>Document.IsInvalid</li>
    ///     <li>Document.IsRequired</li>
    ///     <li>Document.MustBeInformed</li>
    /// </ul>
    /// </response> 
    /// <response code="401"> Unauthorized
    /// <ul>
    ///     <li>Blocked.UserUnauthorized</li>
    /// </ul>
    /// </response> 
    /// <response code="403"> Forbidden
    /// </response>
    /// <response code="409"> Conflict
    /// <ul>
    ///     <li>Idempotency.InProgress</li>
    ///     <li>Idempotency.KeyReuse</li>
    ///     <li>User.HasRegisteredAccount</li>
    /// </ul>
    /// </response>
    /// <response code="500"> InternalServerError
    /// <ul>
    ///     <li>Error.Unexpected</li>
    /// </ul>
    /// </response>
    /// <response code="503"> ServiceUnavailable
    /// <ul>
    ///     <li>Idempotency.Unavailable</li>
    /// </ul>
    /// </response>
    [HttpPost]
    [Authorize]
    [SwaggerRequestExample(typeof(CreateAccountRequest), typeof(CreateAccountRequestExample))]
    [ProducesResponseType(typeof(CreateAccountDto), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> CreateAccountAsync([FromBody] CreateAccountRequest request, [FromHeader(Name = "Idempotency-Key")] Guid requestId)
        => await ExecuteAsync(async () => await _mediatorService.Send(new CreateAccountCommand(requestId, request.Holder, UserId)), HttpStatusCode.Created);

    /// <summary>
    /// api/accounts/deactivate.
    /// </summary>
    /// <remarks>
    /// <p>
    /// <b> Description: </b><br />
    /// This method deactivate an account. <br />
    /// </p>
    /// <p>
    /// <b> Requirements: </b><br />
    /// Is required to be authenticated. <br />
    /// </p>
    /// </remarks>
    /// <response code="204">No Content</response>
    /// <response code="401"> Unauthorized
    /// </response> 
    /// <response code="404"> Not Found
    /// <ul>
    ///     <li>Account.NotFound</li>
    /// </ul>
    /// </response>
    /// <response code="500"> InternalServerError
    /// <ul>
    ///     <li>Error.Unexpected</li>
    /// </ul>
    /// </response>
    [HttpPatch("deactivate")]
    [Authorize]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> DeactivateAccountAsync()
        => await ExecuteAsync(async () => await _mediatorService.Send(new DeactivateAccountCommand(UserId)), HttpStatusCode.NoContent);
}