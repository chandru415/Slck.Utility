using FluentAssertions;
using Xunit;

namespace Slck.Utility.Tests;

public class UriHelpersTests
{
    [Fact]
    public void EnsureTrailingSlash_AddsSlash_WhenMissing()
    {
        UriHelpers.EnsureTrailingSlash("https://example.com/api")
            .Should().Be("https://example.com/api/");
    }

    [Fact]
    public void EnsureTrailingSlash_PreservesSlash_WhenPresent()
    {
        UriHelpers.EnsureTrailingSlash("https://example.com/api/")
            .Should().Be("https://example.com/api/");
    }

    [Fact]
    public void EnsureTrailingSlash_RootPath_ReturnsTrailingSlash()
    {
        UriHelpers.EnsureTrailingSlash("https://example.com")
            .Should().Be("https://example.com/");
    }

    [Fact]
    public void EnsureTrailingSlash_WithPort_PreservesPort()
    {
        UriHelpers.EnsureTrailingSlash("https://example.com:8080/api")
            .Should().Be("https://example.com:8080/api/");
    }

    [Fact]
    public void EnsureTrailingSlash_InvalidUri_ThrowsArgumentException()
    {
        var act = () => UriHelpers.EnsureTrailingSlash("not-a-uri");
        act.Should().Throw<ArgumentException>();
    }
}
