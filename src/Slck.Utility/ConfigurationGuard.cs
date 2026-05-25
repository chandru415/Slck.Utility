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
            if (value.Contains(prefix, StringComparison.OrdinalIgnoreCase))
                return true;
        }

        return false;
    }
}
