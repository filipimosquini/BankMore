using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Authentication.Api.Controllers;

/// <summary>
/// Base controller class.
/// </summary>
public abstract class BaseController<T> : Controller
{
    /// <summary>
    /// Gets or Sets the log service.
    /// </summary>
    protected ILogger _log { get; }

    /// <summary>
    /// Gets or Sets the mediator service.
    /// </summary>
    protected IMediator _mediatorService { get; }


    /// <summary>
    /// The constructor.
    /// </summary>
    /// <param name="loggerFactory"></param>
    /// <param name="mediatorService"></param>
    protected BaseController(ILoggerFactory loggerFactory, IMediator mediatorService)
    {
        _mediatorService = mediatorService;
        _log = loggerFactory.CreateLogger<T>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDataObject">The type of the data object.</typeparam>
    /// <param name="function">The function</param>
    /// <returns></returns>
    protected virtual async Task<IActionResult> ExecuteAsync<TDataObject>(Func<Task<TDataObject>> function)
        => await ExecuteAsync(function, HttpStatusCode.OK);


    /// <summary>
    /// Generate the response asynchronous.
    /// </summary>
    /// <typeparam name="TDataObject">The type of the data object.</typeparam>
    /// <param name="function">The function</param>
    /// <param name="httpStatusCode">The response code.</param>
    /// <returns></returns>
    protected virtual async Task<IActionResult> ExecuteAsync<TDataObject>(Func<Task<TDataObject>> function,
        HttpStatusCode httpStatusCode)
    {
        BaseController<T> baseController = this;

        TDataObject dataObject = await function();

        return baseController.StatusCode((int)httpStatusCode, new
        {
            data = dataObject
        });
    }
}