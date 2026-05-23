using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Slck.Utility;

/// <summary>
/// Lightweight guard clauses for argument validation at system boundaries.
/// </summary>
public static class Guard
{
    /// <summary>
    /// Throws <see cref="ArgumentNullException"/> if <paramref name="value"/> is null.
    /// </summary>
    public static T NotNull<T>([NotNull] T? value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
        where T : class
    {
        ArgumentNullException.ThrowIfNull(value, paramName);
        return value;
    }

    /// <summary>
    /// Throws <see cref="ArgumentException"/> if <paramref name="value"/> is null or whitespace.
    /// </summary>
    public static string NotNullOrWhiteSpace([NotNull] string? value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, paramName);
        return value;
    }

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is not positive.
    /// </summary>
    public static int Positive(int value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        if (value <= 0)
            throw new ArgumentOutOfRangeException(paramName, value, "Value must be positive.");
        return value;
    }

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is negative.
    /// </summary>
    public static int NotNegative(int value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(value, paramName);
        return value;
    }
}
