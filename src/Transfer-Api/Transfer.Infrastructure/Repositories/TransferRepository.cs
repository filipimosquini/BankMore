using Transfer.Core.TransferAggregate.Repositories;
using Transfer.Infrastructure.Contexts;
using Transfer.Infrastructure.Repositories.Bases;

namespace Transfer.Infrastructure.Repositories;

public class TransferRepository(DatabaseContext context) : BaseRepository<Core.TransferAggregate.Transfer>(context), ITransferRepository
{
}