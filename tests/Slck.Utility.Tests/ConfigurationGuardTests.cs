using FluentAssertions;
using Xunit;

namespace Slck.Utility.Tests;

public class ConfigurationGuardTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("CHANGE_ME")]
    [InlineData("change_me")]
    [InlineData("your-secret-here")]
    [InlineData("prefix-your-value")]
    public void IsPlaceholder_WithPlaceholderValues_ReturnsTrue(string? value)
    {
        ConfigurationGuard.IsPlaceholder(value).Should().BeTrue();
    }

    [Theory]
    [InlineData("my-actual-secret")]
    [InlineData("https://keycloak.example.com")]
    [InlineData("abc123")]
    public void IsPlaceholder_WithRealValues_ReturnsFalse(string value)
    {
        ConfigurationGuard.IsPlaceholder(value).Should().BeFalse();
    }
}
