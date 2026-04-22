using Fundamentals.Exercises;

namespace Fundamentals.Tests.Exercises;

// Tests for the exercises in Exercises/Numbers.cs.
// These will fail until the student implements each method.
public class NumbersTests
{
    [Theory]
    [InlineData(3, 4, 7)]
    [InlineData(-1, 1, 0)]
    [InlineData(0, 0, 0)]
    public void Add_ReturnsSum(int a, int b, int expected)
        => Assert.Equal(expected, Numbers.Add(a, b));

    [Theory]
    [InlineData(3.0, 4.0, 12.0)]
    [InlineData(2.5, 4.0, 10.0)]
    public void RectangleArea_ReturnsWidthTimesHeight(double w, double h, double expected)
        => Assert.Equal(expected, Numbers.RectangleArea(w, h));

    [Theory]
    [InlineData(0, 32)]
    [InlineData(100, 212)]
    [InlineData(-40, -40)]
    public void CelsiusToFahrenheit_ConvertsCorrectly(double c, double expected)
        => Assert.Equal(expected, Numbers.CelsiusToFahrenheit(c), precision: 6);
}
