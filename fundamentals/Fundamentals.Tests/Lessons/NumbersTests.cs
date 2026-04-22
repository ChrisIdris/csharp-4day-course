using Fundamentals.Lessons;

namespace Fundamentals.Tests.Lessons;

// Guards for the teaching examples in Lessons/Numbers.cs.
// These should always pass — they protect the lesson content from accidental "fixes".
public class NumbersTests
{
    [Fact]
    public void LessonA_DoubleMultipliesByTwo()
        => Assert.Equal(42, Numbers.Double(21));

    [Fact]
    public void LessonB_CountUpToFiveReturnsFive()
        => Assert.Equal(5, Numbers.CountUpToFive());

    [Fact]
    public void LessonD_IntegerDivisionTruncates()
        => Assert.Equal(3, Numbers.DivideIntegers(7, 2));

    [Fact]
    public void LessonE_DoubleToIntTruncates()
        => Assert.Equal(3, Numbers.DoubleToInt(3.9));

    [Fact]
    public void LessonE_CastingFixesTruncation()
        => Assert.Equal(3.5, Numbers.DivideWithCasting(7, 2));
}
