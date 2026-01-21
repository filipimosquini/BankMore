using Account.Api.Controllers.Requests;
using Account.Api.Documentation.Swagger.Examples;
using Account.Application.Account.Commands.CreateAccount;
using Account.Application.Account.Commands.DeactivateAccount;
using Account.Application.Account.Dto;
using Account.Application.Account.Queries.GetAccountInformationByHolder;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Net;
using System.Threading.Tasks;
using Account.Application.Account.Queries.GetAccountInformation;

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
    ///     <li>Holder.IsRequired</li>
    ///     <li>Holder.MustBeInformed</li>
    ///     <li>Invalid.RequestId</li>
    ///     <li>User.IsRequired</li>
    /// </ul>
    /// </response> 
    /// <response code="401"> Unauthorized
    /// </response> 
    /// <response code="403"> Forbidden
    /// <ul>
    ///     <li>Expired.Token</li>
    ///     <li>Invalid.Token</li>
    /// </ul>
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
    /// <response code="400"> Bad Request
    /// <ul>
    ///     <li>User.IsRequired</li>
    /// </ul>
    /// </response> 
    /// <response code="401"> Unauthorized
    /// </response>
    /// <response code="403"> Forbidden
    /// <ul>
    ///     <li>Expired.Token</li>
    ///     <li>Invalid.Token</li>
    /// </ul>
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

    /// <summary>
    /// api/accounts/information.
    /// </summary>
    /// <remarks>
    /// <p>
    /// <b> Description: </b><br />
    /// This method gets information from account. <br />
    /// </p>
    /// <p>
    /// <b> Requirements: </b><br />
    /// Is required to be authenticated. <br />
    /// </p>
    /// </remarks>
    /// <response code="200">OK</response>
    /// <response code="400">Bad Request
    /// <ul>
    ///     <li>Inactive.Account</li>
    ///     <li>Invalid.AccountNumber</li>
    ///     <li>User.IsRequired</li>
    /// </ul>
    /// </response>
    /// <response code="401"> Unauthorized
    /// </response>
    /// <response code="403"> Forbidden
    /// <ul>
    ///     <li>Expired.Token</li>
    ///     <li>Invalid.Token</li>
    /// </ul>
    /// </response>
    /// <response code="404">Not Found
    /// <ul>
    ///     <li>NotFound.Account</li>
    /// </ul>
    /// </response>
    /// <response code="409"> Conflict
    /// </response>
    /// <response code="500">InternalServerError
    /// <ul>
    ///     <li>Error.Unexpected</li>
    /// </ul>
    /// </response>
    /// <response code="503"> ServiceUnavailable
    /// <ul>
    ///     <li>Idempotency.Unavailable</li>
    /// </ul>
    /// </response>
    [HttpGet("information")]
    [Authorize]
    [SwaggerRequestExample(typeof(GetAccountInformationRequest), typeof(GetAccountInformationRequestExample))]
    [ProducesResponseType(typeof(GetAccountInformationDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAccountInformationAsync([FromQuery] GetAccountInformationRequest request)
        => await ExecuteAsync(async () => await _mediatorService.Send(new GetAccountInformationQuery(request.AccountNumber)), HttpStatusCode.OK);

    /// <summary>
    /// api/accounts/information/holder.
    /// </summary>
    /// <remarks>
    /// <p>
    /// <b> Description: </b><br />
    /// This method gets information from account by holder. <br />
    /// </p>
    /// <p>
    /// <b> Requirements: </b><br />
    /// Is required to be authenticated. <br />
    /// </p>
    /// </remarks>
    /// <response code="200">OK</response>
    /// <response code="400">Bad Request
    /// <ul>
    ///     <li>Inactive.Account</li>
    ///     <li>User.IsRequired</li>
    /// </ul>
    /// </response>
    /// <response code="401"> Unauthorized
    /// </response>
    /// <response code="403"> Forbidden
    /// <ul>
    ///     <li>Expired.Token</li>
    ///     <li>Invalid.Token</li>
    /// </ul>
    /// </response>
    /// <response code="404">Not Found
    /// <ul>
    ///     <li>NotFound.Account</li>
    /// </ul>
    /// </response>
    /// <response code="409"> Conflict
    /// </response>
    /// <response code="500">InternalServerError
    /// <ul>
    ///     <li>Error.Unexpected</li>
    /// </ul>
    /// </response>
    /// <response code="503"> ServiceUnavailable
    /// <ul>
    ///     <li>Idempotency.Unavailable</li>
    /// </ul>
    /// </response>
    [HttpGet("information/holder")]
    [Authorize]
    [ProducesResponseType(typeof(GetAccountInformationDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAccountInformationByHolderAsync()
        => await ExecuteAsync(async () => await _mediatorService.Send(new GetAccountInformationByHolderQuery(UserId)), HttpStatusCode.OK);
}