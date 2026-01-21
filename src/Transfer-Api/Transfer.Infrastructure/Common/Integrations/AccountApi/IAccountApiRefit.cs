using Refit;
using System.Threading;
using System.Threading.Tasks;
using Transfer.Infrastructure.Common.Integrations.AccountApi.Requests;
using Transfer.Infrastructure.Common.Integrations.AccountApi.Responses;

namespace Transfer.Infrastructure.Common.Integrations.AccountApi;


public interface IAccountApiRefit
{
    [Post("/api/v1/movements")]
    Task<ApiResponse<VoidApiResponse>> CreateMovementAsync([Body] MovementRequest request, CancellationToken cancellationToken = default);

    [Get("/api/accounts/information")]
    Task<ApiResponse<InformationAccountResponseEnvelope>> GetInformationAccountAsync([Query] InformationAccountRequest query, CancellationToken cancellationToken = default);

    [Get("/api/accounts/information/holder")]
    Task<ApiResponse<InformationAccountResponseEnvelope>> GetInformationAccountByHolderAsync(CancellationToken cancellationToken = default);
}