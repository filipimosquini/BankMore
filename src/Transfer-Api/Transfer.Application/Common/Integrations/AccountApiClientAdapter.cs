using Refit;
using System;
using System.Threading;
using System.Threading.Tasks;
using Transfer.Infrastructure.Common.Integrations.AccountApi;
using Transfer.Infrastructure.Common.Integrations.AccountApi.Requests;
using Transfer.Infrastructure.Common.Integrations.AccountApi.Responses;

namespace Transfer.Application.Common.Integrations;

public class AccountApiClientAdapter(IAccountApiRefit refit) : IAccountApiClient
{
    public async Task<ApiResponse<VoidApiResponse>> CreateMovementAsync(Guid idempotencyKey, MovementRequest request, CancellationToken cancellationToken)
        => await refit.CreateMovementAsync(idempotencyKey, request, cancellationToken);

    public async Task<ApiResponse<VoidApiResponse>> CreateMovementHolderAsync(Guid idempotencyKey, MovementHolderRequest request, CancellationToken cancellationToken)
        => await refit.CreateMovementHolderAsync(idempotencyKey, request, cancellationToken);

    public async Task<ApiResponse<InformationAccountResponseEnvelope>> GetInformationAccountAsync(InformationAccountRequest query, CancellationToken cancellationToken)
        => await refit.GetInformationAccountAsync(query, cancellationToken);

    public async Task<ApiResponse<InformationAccountResponseEnvelope>> GetInformationAccountByHolderAsync(CancellationToken cancellationToken)
        =>await refit.GetInformationAccountByHolderAsync(cancellationToken);
}