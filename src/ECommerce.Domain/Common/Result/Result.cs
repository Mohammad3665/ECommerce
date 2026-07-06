using System.Diagnostics.CodeAnalysis;
using ECommerce.Domain.Common.Result.Base;

namespace ECommerce.Domain.Common.Result;

#region Result (Without data)
/// <summary>
/// Represents the outcome of an operation that either succeeds or fails, without a return value.
/// Provides an implicit conversion from <see cref="Error"/> to allow direct error propagation.
/// </summary>
public class Result : BaseResult
{
    private Result(bool isSuccess, Error.Error error)
        : base(isSuccess, error) { }

    /// <summary>
    /// Creates a successful result indicating that the operation completed without errors.
    /// </summary>
    /// <returns>A new <see cref="Result"/> representing success.</returns>
    public static Result Success() => new(true, Common.Error.Error.None);

    /// <summary>
    /// Creates a failure result with the specified error.
    /// </summary>
    /// <param name="error">The error describing the failure. Must not be <see cref="Common.Error.Error.None"/>.</param>
    /// <returns>A new <see cref="Result"/> representing failure.</returns>
    public static Result Failure(Error.Error error) => new(false, error);

    /// <summary>
    /// Enables implicit conversion of an <see cref="Error"/> to a failure result,
    /// allowing a method to return an error directly where a <see cref="Result"/> is expected.
    /// </summary>
    /// <param name="error">The error to convert.</param>
    public static implicit operator Result(Error.Error error) => Failure(error);
}
#endregion

#region Result (With data)
/// <summary>
/// Represents the outcome of an operation that either succeeds and returns data,
/// or fails with an error. Implicit conversions from both the success data and an error are provided.
/// </summary>
/// <typeparam name="TData">The type of data returned on success.</typeparam>
public class Result<TData> : BaseResult
{
    private Result(TData? data, bool isSuccess, Error.Error error) : base(isSuccess, error)
    {
        Data = data;
        IsSuccess = isSuccess;
    }

    /// <summary>
    /// Gets the data produced by a successful operation.
    /// When <see cref="IsSuccess"/> is <c>true</c>, this value is guaranteed non‑null (enforced by
    /// <see cref="System.Diagnostics.CodeAnalysis.MemberNotNullWhenAttribute"/>).
    /// </summary>
    public TData? Data { get; }

    /// <summary>
    /// Indicates whether the operation succeeded. Overrides the base property to carry the
    /// <see cref="MemberNotNullWhenAttribute"/> that makes <see cref="Data"/> non‑null after a <c>true</c> check.
    /// </summary>
    [MemberNotNullWhen(true, nameof(Data))]
    public override bool IsSuccess { get; }

    /// <summary>
    /// Creates a successful result containing the provided data.
    /// </summary>
    /// <param name="data">The data to return with the success result.</param>
    /// <returns>A new <see cref="Result{TData}"/> instance representing success.</returns>
    public static Result<TData> Success(TData data) => new(data, true, Common.Error.Error.None);

    /// <summary>
    /// Creates a failure result with the specified error.
    /// </summary>
    /// <param name="error">The error describing the failure. Must not be <see cref="Common.Error.Error.None"/>.</param>
    /// <returns>A new <see cref="Result{TData}"/> instance representing failure.</returns>
    public static Result<TData> Failure(Error.Error error) => new(default, false, error);

    /// <summary>
    /// Enables implicit conversion of a data value to a successful result,
    /// allowing a method to return a value directly where a <see cref="Result{TData}"/> is expected.
    /// </summary>
    /// <param name="data">The data to convert.</param>
    public static implicit operator Result<TData>(TData data) => Success(data);

    /// <summary>
    /// Enables implicit conversion of an <see cref="Error"/> to a failure result,
    /// allowing a method to return an error directly where a <see cref="Result{TData}"/> is expected.
    /// </summary>
    /// <param name="error">The error to convert.</param>
    public static implicit operator Result<TData>(Error.Error error) => Failure(error);
}
#endregion