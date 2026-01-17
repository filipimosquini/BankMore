using Swashbuckle.AspNetCore.Filters;

namespace Transfer.Api.Documentation.Swagger.Examples;

public class DefaultRequestExample : IExamplesProvider<object>
{
    public object GetExamples()
        => string.Empty;
}