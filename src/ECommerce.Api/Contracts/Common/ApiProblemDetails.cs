namespace ECommerce.Api.Contracts.Common;

public class ApiProblemDetails : ProblemDetails
{
    /// <summary>
    /// Gets the collection of validation errors associated with the failed request, if any.
    /// </summary>
    public IEnumerable<ValidationError>? Errors { get; init; }

    /// <summary>
    /// Gets the unique identifier for the request, used to correlate logs and responses.
    /// </summary>
    public string? TraceId { get; init; }

}

/// <summary>
/// Represents a single validation failure, identifying the property that failed validation and the associated error message.
/// </summary>
/// <param name="PropertyName">The name of the property that caused the validation error.</param>
/// <param name="ErrorMessage">A human-readable description of the validation failure.</param>
public sealed record ValidationError(string PropertyName, string ErrorMessage);