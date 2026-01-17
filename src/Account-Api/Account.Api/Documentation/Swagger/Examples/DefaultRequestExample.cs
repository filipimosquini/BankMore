using Swashbuckle.AspNetCore.Filters;

namespace Account.Api.Documentation.Swagger.Examples;

public class DefaultRequestExample : IExamplesProvider<object>
{
    public object GetExamples()
        => string.Empty;
}