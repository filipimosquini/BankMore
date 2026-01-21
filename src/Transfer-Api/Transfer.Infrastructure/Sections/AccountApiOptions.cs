namespace Transfer.Infrastructure.Sections;

public sealed class AccountApiOptions
{
    public const string SectionName = "Integrations:AccountApi";

    public string BaseUrl { get; init; } = default!;
    public int TimeoutSeconds { get; init; } = 10;

    public int RetryCount { get; init; } = 3;
    public int RetryBaseDelayMs { get; init; } = 200;

    public bool CircuitBreakerEnabled { get; init; } = true;
    public int BreakDurationSeconds { get; init; } = 30;
    public double FailureRatio { get; init; } = 0.5;
    public int MinimumThroughput { get; init; } = 20;
    public int SamplingDurationSeconds { get; init; } = 30;
}