using Transfer.Core.Common.Indepotencies.Hashing;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Transfer.Infrastructure.Common.Idempotencies.Hashing;

public class IdempotencyHasher : IIdempotencyHasher
{
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false
    };

    public string Hash<TRequest>(TRequest request)
    {
        var json = JsonSerializer.Serialize(request, Options);
        using var sha256 = SHA256.Create();

        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(json));
        return Convert.ToHexString(bytes);
    }
}