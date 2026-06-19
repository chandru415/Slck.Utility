namespace Slck.Utility;

/// <summary>
/// URI manipulation helpers.
/// </summary>
public static class UriHelpers
{
    /// <summary>
    /// Ensures the given absolute URL ends with a trailing slash on its path segment.
    /// Throws <see cref="ArgumentException"/> if <paramref name="url"/> is not a valid absolute URI.
    /// </summary>
    public static string EnsureTrailingSlash(string url)
    {
        if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
        {
            throw new ArgumentException(
                $"Value must be an absolute URI. Current value: '{url}'.", nameof(url));
        }

        var builder = new UriBuilder(uri)
        {
            Path = string.IsNullOrWhiteSpace(uri.AbsolutePath) || uri.AbsolutePath == "/"
                ? "/"
                : $"{uri.AbsolutePath.TrimEnd('/')}/"
        };

        return builder.Uri.ToString();
    }

    /// <summary>
    /// Ensures the given <see cref="Uri"/> ends with a trailing slash on its path segment.
    /// </summary>
    public static Uri EnsureTrailingSlash(Uri uri)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(uri);
#else
        if (uri is null)
            throw new ArgumentNullException(nameof(uri));
#endif

        var builder = new UriBuilder(uri)
        {
            Path = string.IsNullOrWhiteSpace(uri.AbsolutePath) || uri.AbsolutePath == "/"
                ? "/"
                : $"{uri.AbsolutePath.TrimEnd('/')}/"
        };

        return builder.Uri;
    }
}
