namespace Slck.Utility;

/// <summary>
/// General-purpose parsing and conversion helpers.
/// </summary>
public static class Util
{
    /// <summary>
    /// Parses a required GUID from a string value.
    /// Throws <see cref="ArgumentException"/> if the value is null, whitespace, or not a valid GUID.
    /// </summary>
    /// <param name="value">The string value to parse.</param>
    /// <param name="parameterName">The parameter name (used in the error message).</param>
    public static Guid ParseRequiredGuid(string? value, string parameterName)
    {
        if (!string.IsNullOrWhiteSpace(value) && Guid.TryParse(value, out var guid))
            return guid;

        throw new ArgumentException($"Invalid GUID value for {parameterName}.", parameterName);
    }

    /// <summary>
    /// Parses an optional GUID from a string value.
    /// Returns <c>null</c> if the value is null or whitespace.
    /// Throws <see cref="ArgumentException"/> if the value is present but not a valid GUID.
    /// </summary>
    /// <param name="value">The string value to parse.</param>
    /// <param name="parameterName">The parameter name (used in the error message).</param>
    public static Guid? ParseOptionalGuid(string? value, string parameterName)
    {
        if (string.IsNullOrWhiteSpace(value))
            return null;

        if (Guid.TryParse(value, out var guid))
            return guid;

        throw new ArgumentException($"Invalid GUID value for {parameterName}.", parameterName);
    }
}
