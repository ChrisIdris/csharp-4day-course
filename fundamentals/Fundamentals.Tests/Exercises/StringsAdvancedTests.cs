using Fundamentals.Exercises;

namespace Fundamentals.Tests.Exercises;

// Tests for the advanced exercise in Exercises/StringsAdvanced.cs.
// Will fail until the student implements ParseCsvRow.
public class StringsAdvancedTests
{
    [Fact]
    public void ParseCsvRow_HandlesPlainFields()
        => Assert.Equal(new[] { "one", "two", "three" }, StringsAdvanced.ParseCsvRow("one,two,three"));

    [Fact]
    public void ParseCsvRow_HandlesQuotedFieldWithComma()
        => Assert.Equal(new[] { "Hello, world", "foo", "bar" }, StringsAdvanced.ParseCsvRow("\"Hello, world\",foo,bar"));

    [Fact]
    public void ParseCsvRow_HandlesEmptyFields()
        => Assert.Equal(new[] { "a", "", "b" }, StringsAdvanced.ParseCsvRow("a,,b"));

    [Fact]
    public void ParseCsvRow_HandlesSingleField()
        => Assert.Equal(new[] { "solo" }, StringsAdvanced.ParseCsvRow("solo"));

    [Fact]
    public void ParseCsvRow_HandlesMultipleQuotedFields()
        => Assert.Equal(
            new[] { "a, b", "c, d", "e" },
            StringsAdvanced.ParseCsvRow("\"a, b\",\"c, d\",e"));
}
