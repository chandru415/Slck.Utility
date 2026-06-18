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

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("CHANGE_ME")]
    [InlineData("your-secret")]
    public void Require_WithPlaceholderValues_ThrowsInvalidOperationException(string? value)
    {
        var act = () => ConfigurationGuard.Require(value, "TestKey");
        act.Should().Throw<InvalidOperationException>()
            .WithMessage("*TestKey*");
    }

    [Fact]
    public void Require_WithValidValue_ReturnsValue()
    {
        ConfigurationGuard.Require("real-secret", "TestKey").Should().Be("real-secret");
    }
}
