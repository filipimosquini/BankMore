namespace Account.Core.Common.Indepotencies.Hashing;

public interface IIdempotencyHasher
{
    string Hash<TRequest>(TRequest request);
}