using System;

namespace Account.Core.Common.Indepotencies;

public interface IIdempotencyRequest
{
    /// <summary>
    /// The request Id used for idempotency verification.
    /// </summary>
    /// <example>25561a63-fe01-41fb-bb97-87e4a9b64ac1</example>
    Guid RequestId { get; }
}