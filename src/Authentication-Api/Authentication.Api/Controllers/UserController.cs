using Authentication.Api.Documentation.Swagger.Examples;
using Authentication.Application.Users.Queries.Dto;
using Authentication.Application.Users.Queries.GetUserInformation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Filters;
using System.Net;
using System.Threading.Tasks;

namespace Authentication.Api.Controllers;

[Route("api/users")]
[ApiController]
public class UserController : BaseController<UserController>
{
    public UserController(ILoggerFactory loggerFactory, IMediator mediatorService) 
        : base(loggerFactory, mediatorService)
    {
    }

    /// <summary>
    /// api/users/information.
    /// </summary>
    /// <remarks>
    /// <p>
    /// <b> Description: </b><br />
    /// This method get a user information. <br />
    /// </p>
    /// <p>
    /// <b> Requirements: </b><br />
    /// Is necessary to be authenticated to get information. <br />
    /// </p>
    /// </remarks>
    /// <response code="200">OK</response>
    /// <response code="401"> Unauthorized
    /// <ul>
    ///     <li>Blocked.UserUnauthorized</li>
    /// </ul>
    /// </response> 
    /// <response code="404"> Not Found
    /// <ul>
    ///     <li>User.NotFound</li>
    /// </ul>
    /// </response> 
    /// <response code="500">InternalServerError
    /// <ul>
    ///     <li>Error.Unexpected</li>
    /// </ul>
    /// </response>
    [HttpGet("information")]
    [Authorize]
    [SwaggerRequestExample(typeof(DefaultRequestExample), typeof(DefaultRequestExample))]
    [ProducesResponseType(typeof(UserInformationDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetUserInformationAsync()
        => await ExecuteAsync(async () => await _mediatorService.Send(new GetUserInformationQuery(UserId)), HttpStatusCode.OK);
}