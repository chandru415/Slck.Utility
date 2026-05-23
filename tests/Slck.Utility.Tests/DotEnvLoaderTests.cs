using FluentAssertions;
using Xunit;

namespace Slck.Utility.Tests;

public class DotEnvLoaderTests : IDisposable
{
    private readonly string _tempDir;
    private readonly List<string> _setKeys = [];

    public DotEnvLoaderTests()
    {
        _tempDir = Path.Combine(Path.GetTempPath(), $"slck-test-{Guid.NewGuid():N}");
        Directory.CreateDirectory(_tempDir);
    }

    [Fact]
    public void Load_SetsEnvironmentVariables()
    {
        var envFile = CreateEnvFile("TEST_SLCK_A=hello\nTEST_SLCK_B=world");

        DotEnvLoader.Load(envFile);
        TrackKey("TEST_SLCK_A");
        TrackKey("TEST_SLCK_B");

        Environment.GetEnvironmentVariable("TEST_SLCK_A").Should().Be("hello");
        Environment.GetEnvironmentVariable("TEST_SLCK_B").Should().Be("world");
    }

    [Fact]
    public void Load_DoesNotOverwriteExistingValues()
    {
        Environment.SetEnvironmentVariable("TEST_SLCK_EXISTING", "original");
        TrackKey("TEST_SLCK_EXISTING");

        var envFile = CreateEnvFile("TEST_SLCK_EXISTING=overwritten");

        DotEnvLoader.Load(envFile);

        Environment.GetEnvironmentVariable("TEST_SLCK_EXISTING").Should().Be("original");
    }

    [Fact]
    public void Load_StripsQuotes()
    {
        var envFile = CreateEnvFile("TEST_SLCK_DQ=\"double quoted\"\nTEST_SLCK_SQ='single quoted'");

        DotEnvLoader.Load(envFile);
        TrackKey("TEST_SLCK_DQ");
        TrackKey("TEST_SLCK_SQ");

        Environment.GetEnvironmentVariable("TEST_SLCK_DQ").Should().Be("double quoted");
        Environment.GetEnvironmentVariable("TEST_SLCK_SQ").Should().Be("single quoted");
    }

    [Fact]
    public void Load_IgnoresCommentsAndBlankLines()
    {
        var envFile = CreateEnvFile("# comment\n\n  \nTEST_SLCK_VALID=yes");

        DotEnvLoader.Load(envFile);
        TrackKey("TEST_SLCK_VALID");

        Environment.GetEnvironmentVariable("TEST_SLCK_VALID").Should().Be("yes");
    }

    [Fact]
    public void Load_NonExistentFile_DoesNotThrow()
    {
        var act = () => DotEnvLoader.Load("/nonexistent/path/.env");
        act.Should().NotThrow();
    }

    private string CreateEnvFile(string content)
    {
        var path = Path.Combine(_tempDir, ".env");
        File.WriteAllText(path, content);
        return path;
    }

    private void TrackKey(string key) => _setKeys.Add(key);

    public void Dispose()
    {
        foreach (var key in _setKeys)
            Environment.SetEnvironmentVariable(key, null);

        if (Directory.Exists(_tempDir))
            Directory.Delete(_tempDir, recursive: true);
    }
}
