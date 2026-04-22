using Fundamentals.Exercises;

namespace Fundamentals.Tests.Exercises;

// Tests for the exercises in Exercises/Structs.cs.
// These will fail until the student implements each method.
public class StructsTests
{
    [Fact]
    public void Constructor_AssignsXAndY()
    {
        Point p = new Point(3, 4);
        Assert.Equal(3, p.X);
        Assert.Equal(4, p.Y);
    }

    [Theory]
    [InlineData(0, 0, true)]
    [InlineData(0.0, 0.0, true)]
    [InlineData(1, 0, false)]
    [InlineData(0, 1, false)]
    [InlineData(3, 4, false)]
    public void IsOrigin_TrueOnlyAtZeroZero(double x, double y, bool expected)
        => Assert.Equal(expected, new Point(x, y).IsOrigin());

    [Theory]
    [InlineData(0, 0, 3, 4, 5)]        // classic 3-4-5 triangle
    [InlineData(1, 1, 1, 1, 0)]        // same point → 0
    [InlineData(0, 0, 0, 10, 10)]      // along the Y axis
    [InlineData(-1, -1, 2, 3, 5)]      // negatives
    public void DistanceTo_ReturnsEuclideanDistance(double ax, double ay, double bx, double by, double expected)
    {
        Point a = new Point(ax, ay);
        Point b = new Point(bx, by);
        Assert.Equal(expected, a.DistanceTo(b), precision: 6);
    }

    [Fact]
    public void Translate_ReturnsNewShiftedPoint()
    {
        Point p = new Point(1, 2);
        Point moved = p.Translate(10, 20);
        Assert.Equal(11, moved.X);
        Assert.Equal(22, moved.Y);
    }

    [Fact]
    public void Translate_DoesNotMutateOriginal()
    {
        // Value-type semantics: translating a point must not change the caller's point.
        Point p = new Point(1, 2);
        p.Translate(10, 20); // ignore the return value
        Assert.Equal(1, p.X);
        Assert.Equal(2, p.Y);
    }
}
