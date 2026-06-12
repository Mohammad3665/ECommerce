using ECommerce.Domain.Common.Enums;

namespace ECommerce.Domain.Common.Result.Base;

/// <summary>
/// Serves as the base class for result types that indicate success or failure
/// and carry an associated error when unsuccessful.
/// </summary>
public abstract class BaseResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BaseResult"/> class.
    /// Ensures a consistent state: success must be paired with <see cref="Common.Error.Error.None"/>
    /// and failure must be paired with an actual error.
    /// </summary>
    /// <param name="isSuccess"><c>true</c> if the operation succeeded; otherwise <c>false</c>.</param>
    /// <param name="error">The error associated with the result. Must be <see cref="Common.Error.Error.None"/> when <paramref name="isSuccess"/> is <c>true</c> and must not be <see cref="Common.Error.Error.None"/> when <paramref name="isSuccess"/> is <c>false</c>.</param>
    /// <exception cref="ArgumentException">Thrown when the combination of <paramref name="isSuccess"/> and <paramref name="error"/> is invalid.</exception>
    protected BaseResult(bool isSuccess, Error.Error error)
    {
        if ((isSuccess && error != Common.Error.Error.None) || (!isSuccess && error == Common.Error.Error.None))
            throw new ArgumentException("Invalid error.", nameof(error));
        IsSuccess = isSuccess;
        Error = error;
    }

    /// <summary>
    /// Indicates whether the operation represented by this result was successful.
    /// This property is virtual to allow derived classes to compute success dynamically,
    /// but overriding it must remain consistent with the <see cref="Error"/> property.
    /// </summary>
    public virtual bool IsSuccess { get; }

    /// <summary>
    /// Indicates whether the operation represented by this result failed.
    /// This is the logical negation of <see cref="IsSuccess"/>.
    /// </summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    /// The error that occurred during the operation when <see cref="IsSuccess"/> is <c>false</c>;
    /// otherwise <see cref="Abstractions.Error.Error.None"/>.
    /// </summary>
    public Error.Error Error { get; }
}