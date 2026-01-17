using Account.Infrastructure.CrossCutting.Extensions;
using Account.Infrastructure.CrossCutting.ResourcesCatalog;
using Account.Infrastructure.CrossCutting.ResourcesCatalog.Models;
using Authentication.Infrastructure.CrossCutting.Exceptions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Account.Api.Configurations.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IResourceCatalog _resourceCatalog;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IResourceCatalog resourceCatalog)
    {
        _next = next;
        _logger = logger;
        _resourceCatalog = resourceCatalog;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (AppCustomException exception)
        {
            await HandleAppCustomException(context.Response, exception);
        }
        catch (ValidationException exception)
        {
            await HandleValidationException(context.Response, exception);
        }
        catch (Exception exception)
        {
            await HandleFatalError(context.Response, exception);
        }
    }

    /// <summary>
    /// Handle the exception result when AppCustomException occurs.
    /// </summary>
    /// <param name="exception"> The exception. </param>
    /// <returns></returns>
    private Task HandleAppCustomException(HttpResponse response, AppCustomException exception)
    {
        _logger.LogError(new
        {
            timestamp = DateTime.UtcNow,
            correlation = Guid.NewGuid().ToString(),
            StackTrace = exception.StackTrace
        }.ToJson());

        response.ContentType = "application/json";
        response.StatusCode = (int) exception.HttpStatusCode;

        if (string.IsNullOrWhiteSpace(exception.Message))
        {
            return response.WriteAsync(new { notifications = _resourceCatalog.UnexpectedError() }.ToJson());
        }

        var _notifications = new List<Notification>();
        var notificationsFromFile = _resourceCatalog.Get(exception.Message) ?? _resourceCatalog.UnexpectedError();

        if (notificationsFromFile.Any())
        {
            foreach (var notification in notificationsFromFile)
            {
                _notifications.Add(new Notification
                {
                    Code = notification.Code,
                    Message = notification.Message
                });
            }
        }

        return response.WriteAsync(new { notifications = _notifications }.ToJson());
    }

   /// <summary>
    /// Handle the exception result when ValidationException occurs.
    /// </summary>
    /// <param name="exception"> The exception. </param>
    /// <returns></returns>
    private Task HandleValidationException(HttpResponse response, ValidationException exception)
    {
        response.ContentType = "application/json";
        response.StatusCode = (int)HttpStatusCode.BadRequest;

        var notifications = new List<Notification>();

        foreach (ValidationFailure error in exception.Errors)
        {
            var notificationsFromFile = _resourceCatalog.Get(error.ErrorCode);

            if (notificationsFromFile.Any())
            {
                foreach (var notification in notificationsFromFile)
                {
                    notifications.Add(new Notification
                    {
                        Code = notification.Code,
                        Message = notification.Message
                    });
                }
            }
            else
            {
                notifications.Add(new Notification
                {
                    Code = error.ErrorCode,
                    Message = error.ErrorMessage
                });
            }
        }

        return response.WriteAsync(new { notifications = notifications }.ToJson());
    }

    /// <summary>
    /// Handle the exception result when fatal error occurs.
    /// </summary>
    /// <param name="exception"> The exception. </param>
    /// <returns></returns>
    private Task HandleFatalError(HttpResponse response, Exception exception)
    {
        _logger.LogCritical(new
        {
            timestamp = DateTime.UtcNow,
            correlation = Guid.NewGuid().ToString(),
            StackTrace = exception.StackTrace
        }.ToJson());

        response.ContentType = "application/json";
        response.StatusCode = (int)HttpStatusCode.InternalServerError;

        return response.WriteAsync(new { notifications = _resourceCatalog.UnexpectedError() }.ToJson());
    }
}