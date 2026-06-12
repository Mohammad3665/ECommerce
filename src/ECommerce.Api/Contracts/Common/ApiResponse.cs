namespace ECommerce.Api.Contracts.Common;

#region Api Response (Without data)
/// <summary>
/// Represents a standard API response indicating success or failure without associated data.
/// </summary>
/// <param name="Succeeded">Indicates whether the operation succeeded.</param>
/// <param name="Message">An optional message providing additional context (e.g., success details or error description).</param>
public sealed record ApiResponse(bool Succeeded, string? Message)
{
    /// <summary>
    /// Creates a success response with an optional message.
    /// </summary>
    /// <param name="message">An optional informational message.</param>
    /// <returns>A new <see cref="ApiResponse"/> with <paramref name="Succeeded"/> set to <c>true</c>.</returns>
    public static ApiResponse Success(string? message = null) => new(true, message);   
}
#endregion

#region Api Response (With data)
/// <summary>
/// Represents a standard API response that includes typed data on success.
/// </summary>
/// <typeparam name="T">The type of the data payload.</typeparam>
/// <param name="Data">The payload returned by a successful operation. Can be <c>null</c> when <paramref name="Succeeded"/> is <c>true</c> if the operation produces no data.</param>
/// <param name="Succeeded">Indicates whether the operation succeeded.</param>
/// <param name="Message">An optional message providing additional context.</param>
public sealed record ApiResponse<T>(T? Data, bool Succeeded, string? Message)
{
    /// <summary>
    /// Creates a success response containing the provided data and an optional message.
    /// </summary>
    /// <param name="data">The data payload to return.</param>
    /// <param name="message">An optional informational message.</param>
    /// <returns>A new <see cref="ApiResponse{T}"/> with <paramref name="Succeeded"/> set to <c>true</c>.</returns>
    public static ApiResponse<T> Success(T? data, string? message = null) => new(data, true, message);
}
#endregion