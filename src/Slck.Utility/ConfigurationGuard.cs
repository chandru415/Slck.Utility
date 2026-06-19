namespace Slck.Utility;

/// <summary>
/// Helpers for validating configuration values at application startup.
/// </summary>
public static class ConfigurationGuard
{
    private static readonly string[] PlaceholderTokens = ["CHANGE_ME"];
    private static readonly string[] PlaceholderPrefixes = ["your-"];

    /// <summary>
    /// Returns <c>true</c> if the value is null, whitespace, or contains a well-known placeholder token.
    /// </summary>
    public static bool IsPlaceholder(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return true;

        foreach (var token in PlaceholderTokens)
        {
            if (string.Equals(value, token, StringComparison.OrdinalIgnoreCase))
                return true;
        }

        foreach (var prefix in PlaceholderPrefixes)
        {
            if (value!.IndexOf(prefix, StringComparison.OrdinalIgnoreCase) >= 0)
                return true;
        }

        return false;
    }

    /// <summary>
    /// Validates that the configuration value is not a placeholder and returns it.
    /// Throws <see cref="InvalidOperationException"/> if the value is missing or contains a placeholder.
    /// </summary>
    /// <param name="value">The configuration value to validate.</param>
    /// <param name="key">The configuration key name (used in the error message).</param>
    /// <returns>The validated, non-placeholder value.</returns>
    public static string Require(string? value, string key)
    {
        if (IsPlaceholder(value))
            throw new InvalidOperationException(
                $"Configuration key '{key}' is missing or contains a placeholder value.");
        return value!;
    }
}
