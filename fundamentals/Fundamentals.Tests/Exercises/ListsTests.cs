using Fundamentals.Exercises;

namespace Fundamentals.Tests.Exercises;

// Tests for the exercises in Exercises/Lists.cs.
// These will fail until the student implements each method.
public class ListsTests
{
    [Theory]
    [InlineData(new[] { 1, 2, 3, 4 }, 10)]
    [InlineData(new int[0], 0)]
    [InlineData(new[] { -5, 5 }, 0)]
    [InlineData(new[] { 7 }, 7)]
    public void SumOfList_SumsAllElements(int[] input, int expected)
        => Assert.Equal(expected, Lists.SumOfList(new List<int>(input)));

    [Theory]
    [InlineData(new[] { 3, 1, 4, 1, 5, 9, 2, 6 }, 9)]
    [InlineData(new[] { -10, -20, -5 }, -5)]
    [InlineData(new[] { 42 }, 42)]
    public void FindLargest_ReturnsMax(int[] input, int expected)
        => Assert.Equal(expected, Lists.FindLargest(new List<int>(input)));

    [Theory]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, 3)]
    [InlineData(new[] { 1, 3, 5 }, 0)]
    [InlineData(new[] { 2, 4, 6 }, 3)]
    [InlineData(new int[0], 0)]
    [InlineData(new[] { 0 }, 1)]
    public void CountEvens_CountsEvenNumbers(int[] input, int expected)
        => Assert.Equal(expected, Lists.CountEvens(new List<int>(input)));

    [Theory]
    [InlineData(new[] { 3, -1, 0, 7, -5 }, 2)]
    [InlineData(new[] { -1, -2, -3 }, 0)]
    [InlineData(new[] { 1, 2, 3 }, 3)]
    [InlineData(new int[0], 0)]
    public void CountPositives_CountsStrictlyPositive(int[] input, int expected)
        => Assert.Equal(expected, Lists.CountPositives(new List<int>(input)));

    [Theory]
    [InlineData(new[] { 10, 20, 30, 20 }, 20, 1)]
    [InlineData(new[] { 1, 2, 3 }, 9, -1)]
    [InlineData(new[] { 5 }, 5, 0)]
    [InlineData(new int[0], 1, -1)]
    public void FirstIndexOf_ReturnsFirstMatchOrMinusOne(int[] input, int target, int expected)
        => Assert.Equal(expected, Lists.FirstIndexOf(new List<int>(input), target));

    [Theory]
    [InlineData(new[] { 1, 2, 3, -1, 10 }, 6)]
    [InlineData(new[] { 1, 2, 3 }, 6)]
    [InlineData(new[] { -1, 5, 5 }, 0)]
    [InlineData(new int[0], 0)]
    public void SumUntilNegative_StopsAtFirstNegative(int[] input, int expected)
        => Assert.Equal(expected, Lists.SumUntilNegative(new List<int>(input)));

    [Theory]
    [InlineData(new[] { 1, 2, 3 }, new[] { 3, 2, 1 })]
    [InlineData(new[] { 1 }, new[] { 1 })]
    [InlineData(new int[0], new int[0])]
    [InlineData(new[] { 1, 2, 3, 4 }, new[] { 4, 3, 2, 1 })]
    public void ReverseList_ReturnsReversedCopy(int[] input, int[] expected)
        => Assert.Equal(expected, Lists.ReverseList(new List<int>(input)));

    [Fact]
    public void ReverseList_DoesNotMutateInput()
    {
        List<int> input = new List<int> { 1, 2, 3 };
        List<int> snapshot = new List<int> { 1, 2, 3 };
        Lists.ReverseList(input);
        Assert.Equal(snapshot, input);
    }
}
