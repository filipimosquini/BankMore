using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;

namespace Transfer.Api.Configurations.Extensions;

public static class ResponseCompressionSetup
{
    public static IServiceCollection AddingResponseCompression(this IServiceCollection services)
    {
        return services.AddResponseCompression(options =>
        {
            options.Providers.Add<GzipCompressionProvider>();
            options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/json" });
        });
    }
}