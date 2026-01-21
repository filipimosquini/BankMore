using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Authentication.Infrastructure.CrossCutting.Exceptions;
using Authentication.Infrastructure.CrossCutting.Extensions;
using Authentication.Infrastructure.CrossCutting.ResourcesCatalog;
using Authentication.Infrastructure.CrossCutting.ResourcesCatalog.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Authentication.Api.Configuration.Middlewares;

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
        catch (AppNotificationBaseException exception)
        {
            await HandleAppCustomException(context.Response, exception);
        }
        catch (IdentityErrorCustomException exception)
        {
            await HandleIdentityErrorCustomException(context.Response, exception);
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
    /// Handle the exception result when AppNotificationBaseException occurs.
    /// </summary>
    /// <param name="exception"> The exception. </param>
    /// <returns></returns>
    private Task HandleAppCustomException(HttpResponse response, AppNotificationBaseException exception)
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
    /// Handle the exception result when IdentityErrorCustomException occurs.
    /// </summary>
    /// <param name="exception"> The exception. </param>
    /// <returns></returns>
    private Task HandleIdentityErrorCustomException(HttpResponse response, IdentityErrorCustomException exception)
    {
        _logger.LogError(new
        {
            timestamp = DateTime.UtcNow,
            correlation = Guid.NewGuid().ToString(),
            StackTrace = exception.StackTrace
        }.ToJson());

        response.ContentType = "application/json";
        response.StatusCode = (int)exception.HttpStatusCode;

        if (string.IsNullOrWhiteSpace(exception.Message))
        {
            return response.WriteAsync(new { notifications = _resourceCatalog.UnexpectedError() }.ToJson());
        }

        var _notifications = new List<Notification>();

        foreach (var error in exception.Errors)
        {
            var notifications = _resourceCatalog.Get(error.Code);

            if (!notifications.Any())
            {
                _notifications.Add(new Notification
                {
                    Code = error.Code,
                    Message = error.Description
                });
            }

            foreach (var notification in notifications)
            {
                _notifications.Add(notification);
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