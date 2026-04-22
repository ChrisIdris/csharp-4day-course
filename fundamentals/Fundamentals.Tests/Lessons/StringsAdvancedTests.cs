using Fundamentals.Lessons;

namespace Fundamentals.Tests.Lessons;

// Guards for the teaching examples in Lessons/StringsAdvanced.cs.
// The three join variants should all produce the same output.
public class StringsAdvancedTests
{
    [Fact]
    public void LessonG_ConcatenationProducesJoinedString()
        => Assert.Equal("onetwothree", StringsAdvanced.JoinWithConcatenation(["one", "two", "three"]));

    [Fact]
    public void LessonG_StringBuilderProducesJoinedString()
        => Assert.Equal("onetwothree", StringsAdvanced.JoinWithStringBuilder(["one", "two", "three"]));

    [Fact]
    public void LessonG_StringJoinProducesJoinedString()
        => Assert.Equal("onetwothree", StringsAdvanced.JoinWithStringJoin(["one", "two", "three"]));
}
