using System.Threading;
using System.Threading.Tasks;
using Transfer.Infrastructure.Common.Integrations.AccountApi.Requests;

namespace Transfer.Application.Common.Integrations;

public interface IAccountApiClient
{
    Task CreateMovementAsync(MovementRequest request, CancellationToken cancellationToken);
}