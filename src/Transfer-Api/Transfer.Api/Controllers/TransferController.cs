using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Net;
using System.Threading.Tasks;
using Transfer.Api.Controllers.Requests;
using Transfer.Api.Documentation.Swagger.Examples;
using Transfer.Application.Transfer.Commands.CreateTransfer;


namespace Transfer.Api.Controllers;

[Route("api/transfers")]
public class TransferController(ILoggerFactory loggerFactory, IMediator mediatorService) : BaseController<TransferController>(loggerFactory, mediatorService)
{
    /// <summary>
    /// api/transfers.
    /// </summary>
    /// <remarks>
    /// <p>
    /// <b> Description: </b><br />
    /// This method create a transfer. <br />
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
    ///     <li>Invalid.DestinationAccountNumber</li>
    ///     <li>Invalid.Amount</li>
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
    /// <response code="404">Not Found
    /// <ul>
    ///     <li>NotFound.Account</li>
    /// </ul>
    /// </response>
    /// <response code="409"> Conflict
    /// <ul>
    ///     <li>Equals.SourceAndDestinationAccount</li>
    ///     <li>Idempotency.InProgress</li>
    ///     <li>Idempotency.KeyReuse</li>
    /// </ul>
    /// </response>
    /// <response code="422"> Unprocessable Entity
    /// <ul>
    ///     <li>NotRegistered.Movement</li>
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
    [SwaggerRequestExample(typeof(CreateTransferRequest), typeof(CreateTransferRequestExample))]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> CreateTransferAsync([FromBody] CreateTransferRequest request, [FromHeader(Name = "Idempotency-Key")] Guid requestId)
        => await ExecuteAsync(async () => await _mediatorService.Send(new CreateTransferCommand(requestId, request.DestinationAccountNumber, request.Amount, UserId)), HttpStatusCode.NoContent);
}