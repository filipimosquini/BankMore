using Account.Core.Common.Indepotencies;
using Account.Core.Common.Indepotencies.Entities;
using Account.Core.Common.Indepotencies.Hashing;
using Account.Core.Common.Indepotencies.Repositories;
using Account.Infrastructure.CrossCutting.Exceptions;
using Account.Infrastructure.CrossCutting.ResourcesCatalog;
using Account.Infrastructure.CrossCutting.ResourcesCatalog.Models;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Account.Infrastructure.CrossCutting.Extensions;

namespace Account.Application.Common.Idempotencies.Behaviors;

/// <summary>
/// Idempotency behavior for mediator pipeline class.
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
/// <param name="repository"></param>
/// <param name="hasher"></param>
/// <param name="resourceCatalog"></param>
public sealed class IdempotencyBehavior<TRequest, TResponse>(IIdempotencyRepository repository, IIdempotencyHasher hasher, IResourceCatalog resourceCatalog) : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private static readonly JsonSerializerSettings SerializerSettings = new()
    {
        NullValueHandling = NullValueHandling.Ignore,
        Formatting = Formatting.None
    };

    /// <summary>
    /// Handles the request idempotency.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="next"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request is not IIdempotencyRequest idempotent)
            return await next();

        var hash = hasher.Hash(request);

        bool started;
        try
        {
            started = await repository.TryBeginAsync(idempotent.RequestId, hash, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new IdempotencyUnavailableServiceUnavailableException(ex);
        }

        if (!started)
        {
            Idempotency? existing;
            try
            {
                existing = await repository.GetAsync(idempotent.RequestId, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new IdempotencyUnavailableServiceUnavailableException(ex);
            }

            if (existing is null)
                return await next();

            if (existing.RequestHash != hash)
                throw new IdempotencyKeyReuseConflictException();

            if (existing.Result is null)
                throw new IdempotencyInProgressConflictException();

            if (LooksLikeNotificationError(existing.Result, out var notifications))
            {
                throw new IdempotencyNotificationBaseException(notifications, HttpStatusCode.Conflict);
            }

            if (typeof(TResponse) == typeof(Unit))
                return (TResponse)(object)Unit.Value;

            return JsonConvert.DeserializeObject<TResponse>(existing.Result, SerializerSettings)!;
        }

        TResponse response = default!;
        try
        {
            response = await next();
        }
        catch (Exception exception)
        {
            GetNotificationsFromErrorMessage(exception, out var notifications);

            try
            {
                await repository.CompleteAsync(idempotent.RequestId, 
                    new { notifications = notifications }.ToJson(), cancellationToken);
            }
            catch (Exception ex)
            {
                throw new IdempotencyUnavailableServiceUnavailableException(ex);
            }

            throw;
        }

        var resultJson = typeof(TResponse) == typeof(Unit) ? "{}" : JsonConvert.SerializeObject(response, SerializerSettings);

        try
        {
            await repository.CompleteAsync(idempotent.RequestId, resultJson, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new IdempotencyUnavailableServiceUnavailableException(ex);
        }

        return response;
    }

    private void GetNotificationsFromErrorMessage(Exception exception, out List<Notification> notifications)
    {
        notifications = new List<Notification>();
        var notificationsFromFile = resourceCatalog.Get(exception.Message) ?? resourceCatalog.UnexpectedError();

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
    }

    private static bool LooksLikeNotificationError(string json, out NotificationEnvelope notifications)
    {
        notifications = new NotificationEnvelope()
        {
            Notifications = new List<Notification>()
        };

        try
        {
            var envelope = JsonConvert.DeserializeObject<NotificationEnvelope>(json, SerializerSettings);

            if (envelope?.Notifications is { Count: > 0 })
            {
                notifications = envelope;
                return true;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }
}
