using FluentAssertions;
using Xunit;

namespace Slck.Utility.Tests;

public class UtilTests
{
    [Fact]
    public void ParseRequiredGuid_WithValidGuid_ReturnsGuid()
    {
        var expected = Guid.NewGuid();
        Util.ParseRequiredGuid(expected.ToString(), "id").Should().Be(expected);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("not-a-guid")]
    public void ParseRequiredGuid_WithInvalidValue_ThrowsArgumentException(string? value)
    {
        var act = () => Util.ParseRequiredGuid(value, "id");
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void ParseOptionalGuid_WithValidGuid_ReturnsGuid()
    {
        var expected = Guid.NewGuid();
        Util.ParseOptionalGuid(expected.ToString(), "id").Should().Be(expected);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void ParseOptionalGuid_WithNullOrWhiteSpace_ReturnsNull(string? value)
    {
        Util.ParseOptionalGuid(value, "id").Should().BeNull();
    }

    [Fact]
    public void ParseOptionalGuid_WithInvalidNonEmptyValue_ThrowsArgumentException()
    {
        var act = () => Util.ParseOptionalGuid("not-a-guid", "id");
        act.Should().Throw<ArgumentException>();
    }
}
