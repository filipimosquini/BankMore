using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;
using Microsoft.Extensions.Options;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using Refit;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Transfer.Infrastructure.Common.Integrations.AccountApi;
using Transfer.Infrastructure.Sections;

namespace Transfer.Infrastructure.Common.Integrations.Configurations;

public static class IntegrationConfiguration
{
    public static IHttpResiliencePipelineBuilder AddIntegrations(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddRefitClient<IAccountApiRefit>()
            .ConfigureHttpClient((sp, client) =>
            {
                var opt = sp.GetRequiredService<IOptions<AccountApiOptions>>().Value;
                client.BaseAddress = new Uri(opt.BaseUrl);
                client.Timeout = TimeSpan.FromSeconds(Math.Max(1, opt.TimeoutSeconds));
            })
            .AddHttpMessageHandler<BearerTokenForwardingHandler>()
            .AddResilienceHandler("AccountApi", static (builder, context) =>
            {
                var opt = context.ServiceProvider
                    .GetRequiredService<IOptions<AccountApiOptions>>()
                    .Value;

                builder.AddTimeout(TimeSpan.FromSeconds(Math.Max(1, opt.TimeoutSeconds)));

                builder.AddRetry(new RetryStrategyOptions<HttpResponseMessage>
                {
                    MaxRetryAttempts = Math.Max(0, opt.RetryCount),
                    Delay = TimeSpan.FromMilliseconds(Math.Max(0, opt.RetryBaseDelayMs)),
                    BackoffType = DelayBackoffType.Exponential,
                    UseJitter = true,
                    ShouldHandle = static args =>
                    {
                        var outcome = args.Outcome;

                        if (outcome.Exception is HttpRequestException)
                            return ValueTask.FromResult(true);

                        var response = outcome.Result;
                        if (response is null)
                            return ValueTask.FromResult(false);

                        var code = (int)response.StatusCode;
                        var is5xx = code is >= 500 and <= 599;

                        return ValueTask.FromResult(response.StatusCode == HttpStatusCode.RequestTimeout ||
                                                    response.StatusCode == HttpStatusCode.TooManyRequests ||
                                                    is5xx);
                    }
                });

                if (opt.CircuitBreakerEnabled)
                {
                    builder.AddCircuitBreaker(new CircuitBreakerStrategyOptions<HttpResponseMessage>
                    {
                        BreakDuration = TimeSpan.FromSeconds(Math.Max(1, opt.BreakDurationSeconds)),
                        FailureRatio = opt.FailureRatio,
                        MinimumThroughput = Math.Max(1, opt.MinimumThroughput),
                        SamplingDuration = TimeSpan.FromSeconds(Math.Max(1, opt.SamplingDurationSeconds)),

                        ShouldHandle = static args =>
                        {
                            var outcome = args.Outcome;

                            if (outcome.Exception is HttpRequestException)
                                return ValueTask.FromResult(true);

                            var response = outcome.Result;
                            if (response is null)
                                return ValueTask.FromResult(false);

                            var code = (int)response.StatusCode;
                            var is5xx = code is >= 500 and <= 599;

                            return ValueTask.FromResult(response.StatusCode == HttpStatusCode.RequestTimeout || is5xx);
                        }
                    });
                }
            });
    }
}