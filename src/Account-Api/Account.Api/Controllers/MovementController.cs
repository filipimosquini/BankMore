using Account.Api.Controllers.Requests;
using Account.Api.Documentation.Swagger.Examples;
using Account.Application.Movement.Commands.CreateMovement;
using Account.Application.Movement.Dto;
using Account.Application.Movement.Queries.GetBalances;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Account.Api.Controllers;

[Route("api/movements")]
public class MovementController(ILoggerFactory loggerFactory, IMediator mediatorService) : BaseController<MovementController>(loggerFactory, mediatorService)
{
    /// <summary>
    /// api/movements.
    /// </summary>
    /// <remarks>
    /// <p>
    /// <b> Description: </b><br />
    /// This method create a movement. <br />
    /// </p>
    /// <p>
    /// <b> Requirements: </b><br />
    /// Is required to be authenticated. <br />
    /// </p>
    /// </remarks>
    /// <response code="204">No Content</response>
    /// <response code="400">Bad Request
    /// <ul>
    ///     <li>Inactive.Account</li>
    ///     <li>Invalid.AccountNumber</li>
    ///     <li>Invalid.Amount</li>
    ///     <li>Invalid.MovementType</li>
    ///     <li>Invalid.RequestId</li>
    /// </ul>
    /// </response>
    /// <response code="401"> Unauthorized
    /// <ul>
    ///     <li>Blocked.UserUnauthorized</li>
    /// </ul>
    /// </response> 
    /// <response code="404">Not Found
    /// <ul>
    ///     <li>NotFound.Account</li>
    /// </ul>
    /// </response>
    /// <response code="409"> Conflict
    /// <ul>
    ///     <li>Conflict.OnlyCreditMovementAccepted</li>
    ///     <li>Idempotency.InProgress</li>
    ///     <li>Idempotency.KeyReuse</li>
    /// </ul>
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
    [HttpPost]
    [Authorize]
    [SwaggerRequestExample(typeof(CreateMovementRequest), typeof(CreateMovementRequestExample))]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> CreateMovementAsync([FromBody] CreateMovementRequest request, [FromHeader(Name = "Idempotency-Key")] Guid requestId)
        => await ExecuteAsync(async () => await _mediatorService.Send(new CreateMovementCommand(requestId, request.AccountNumber, request.Amount, request.MovementType, UserId)), HttpStatusCode.NoContent);

    /// <summary>
    /// api/movements/balances.
    /// </summary>
    /// <remarks>
    /// <p>
    /// <b> Description: </b><br />
    /// This method get a balance result. <br />
    /// </p>
    /// <p>
    /// <b> Requirements: </b><br />
    /// Is required to be authenticated. <br />
    /// </p>
    /// </remarks>
    /// <response code="200"> OK </response>
    /// <response code="400"> Bad Request
    /// <ul>
    /// <li>Inactive.Account</li>
    /// <li>Invalid.AccountNumber</li>
    /// </ul>
    /// </response>
    /// <response code="401"> Unauthorized
    /// <ul>
    ///     <li>Blocked.UserUnauthorized</li>
    /// </ul>
    /// </response> 
    /// <response code="404">Not Found
    /// <ul>
    ///     <li>NotFound.Account</li>
    /// </ul>
    /// </response>
    /// <response code="500"> InternalServerError
    /// <ul>
    /// <li> Error.Unexpected </li>
    /// </ul>
    /// </response>
    [HttpGet("balances")]
    [Authorize]
    [ProducesResponseType(typeof(GetBalanceQueryDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetBalanceAsync([FromQuery] GetBalanceQuery query)
        => await ExecuteAsync(async () => await _mediatorService.Send(query));
}