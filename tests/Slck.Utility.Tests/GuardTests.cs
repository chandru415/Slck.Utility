using FluentAssertions;

namespace Slck.Utility.Tests;

public class GuardTests
{
    [Fact]
    public void NotNull_WithValue_ReturnsValue()
    {
        var obj = new object();
        Guard.NotNull(obj).Should().BeSameAs(obj);
    }

    [Fact]
    public void NotNull_WithNull_ThrowsArgumentNullException()
    {
        object? obj = null;
        var act = () => Guard.NotNull(obj);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void NotNullOrWhiteSpace_WithValue_ReturnsValue()
    {
        Guard.NotNullOrWhiteSpace("hello").Should().Be("hello");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void NotNullOrWhiteSpace_WithInvalid_ThrowsArgumentException(string? value)
    {
        var act = () => Guard.NotNullOrWhiteSpace(value);
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Positive_WithPositiveValue_ReturnsValue()
    {
        Guard.Positive(5).Should().Be(5);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Positive_WithNonPositive_ThrowsArgumentOutOfRangeException(int value)
    {
        var act = () => Guard.Positive(value);
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void NotNegative_WithZero_ReturnsValue()
    {
        Guard.NotNegative(0).Should().Be(0);
    }

    [Fact]
    public void NotNegative_WithNegative_ThrowsArgumentOutOfRangeException()
    {
        var act = () => Guard.NotNegative(-1);
        act.Should().Throw<ArgumentOutOfRangeException>();
    }
}
