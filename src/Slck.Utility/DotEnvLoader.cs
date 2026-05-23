namespace Slck.Utility;

/// <summary>
/// Loads environment variables from a <c>.env</c> file before the host is built,
/// so values are visible to <c>AddEnvironmentVariables()</c> configuration providers.
/// <para>
/// Respects the <c>DOTNET_ENV_FILE</c> environment variable to override the file path.
/// Never overwrites variables already present in the process environment (CI/CD wins).
/// </para>
/// </summary>
public static class DotEnvLoader
{
    /// <summary>
    /// Loads variables from the configured .env file.
    /// File path is resolved from <c>DOTNET_ENV_FILE</c> env var (defaults to <c>.env</c>).
    /// Searches <see cref="Directory.GetCurrentDirectory"/> first, then <see cref="AppContext.BaseDirectory"/>.
    /// </summary>
    public static void LoadFromConfiguredFile()
    {
        var envFile = Environment.GetEnvironmentVariable("DOTNET_ENV_FILE");
        if (string.IsNullOrWhiteSpace(envFile))
            envFile = ".env";

        Load(envFile);
    }

    /// <summary>
    /// Loads variables from the specified .env file path (relative or absolute).
    /// </summary>
    /// <param name="filePath">Relative or absolute path to the .env file.</param>
    public static void Load(string filePath)
    {
        var path = ResolveFilePath(filePath);
        if (path is null) return;

        foreach (var rawLine in File.ReadAllLines(path))
        {
            var line = rawLine.Trim();
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith('#'))
                continue;

            var separatorIndex = line.IndexOf('=');
            if (separatorIndex <= 0)
                continue;

            var key = line[..separatorIndex].Trim();
            var value = UnquoteValue(line[(separatorIndex + 1)..].Trim());

            // Never overwrite existing environment variables — CI/CD values always win.
            if (!string.IsNullOrWhiteSpace(key)
                && string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable(key)))
            {
                Environment.SetEnvironmentVariable(key, value);
            }
        }
    }

    private static string? ResolveFilePath(string filePath)
    {
        if (Path.IsPathRooted(filePath) && File.Exists(filePath))
            return filePath;

        var candidates = new[]
        {
            Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), filePath)),
            Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, filePath))
        }.Distinct(StringComparer.OrdinalIgnoreCase);

        return candidates.FirstOrDefault(File.Exists);
    }

    private static string UnquoteValue(string value)
    {
        if (value.Length >= 2)
        {
            if ((value.StartsWith('"') && value.EndsWith('"'))
                || (value.StartsWith('\'') && value.EndsWith('\'')))
            {
                return value[1..^1];
            }
        }

        return value;
    }
}
