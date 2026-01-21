using Refit;
using System;
using System.Threading;
using System.Threading.Tasks;
using Transfer.Infrastructure.Common.Integrations.AccountApi.Requests;
using Transfer.Infrastructure.Common.Integrations.AccountApi.Responses;

namespace Transfer.Application.Common.Integrations;

public interface IAccountApiClient
{
    Task<ApiResponse<VoidApiResponse>> CreateMovementAsync(Guid idempotencyKey, MovementRequest request, CancellationToken cancellationToken);
    Task<ApiResponse<VoidApiResponse>> CreateMovementHolderAsync(Guid idempotencyKey, MovementHolderRequest request, CancellationToken cancellationToken);
    Task<ApiResponse<InformationAccountResponseEnvelope>> GetInformationAccountAsync(InformationAccountRequest query, CancellationToken cancellationToken);
    Task<ApiResponse<InformationAccountResponseEnvelope>> GetInformationAccountByHolderAsync(CancellationToken cancellationToken);
}