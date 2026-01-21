using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Transfer.Infrastructure.Common.Integrations.AccountApi;

public class BearerTokenForwardingHandler(IHttpContextAccessor httpContextAccessor) : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) 
    {
        var ctx = httpContextAccessor.HttpContext;
        var auth = ctx?.Request?.Headers["Authorization"].ToString();

        if (!string.IsNullOrWhiteSpace(auth) && request.Headers.Authorization is null)
        {
            if (AuthenticationHeaderValue.TryParse(auth, out var headerValue))
                request.Headers.Authorization = headerValue;
        }

        return base.SendAsync(request, cancellationToken);
    }
}