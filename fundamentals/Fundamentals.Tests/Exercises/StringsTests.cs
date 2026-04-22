using System.Globalization;
using Fundamentals.Exercises;

namespace Fundamentals.Tests.Exercises;

// Tests for the exercises in Exercises/Strings.cs.
// These will fail until the student implements each method.
public class StringsTests
{
    public StringsTests()
    {
        // Pin culture so FormatPrice output matches on any host locale.
        CultureInfo.CurrentCulture = new CultureInfo("en-GB");
    }

    [Theory]
    [InlineData("hello", "HELLO!!!")]
    [InlineData("Hi", "HI!!!")]
    [InlineData("", "!!!")]
    public void Shout_UppercasesAndAppendsBang(string input, string expected)
        => Assert.Equal(expected, Strings.Shout(input));

    [Theory]
    [InlineData("Hello World", 3)]
    [InlineData("AEIOU", 5)]
    [InlineData("rhythm", 0)]
    [InlineData("", 0)]
    public void CountVowels_CountsVowelsIgnoringCase(string input, int expected)
        => Assert.Equal(expected, Strings.CountVowels(input));

    [Theory]
    [InlineData("Racecar", true)]
    [InlineData("level", true)]
    [InlineData("hello", false)]
    [InlineData("a", true)]
    public void IsPalindrome_IgnoresCase(string input, bool expected)
        => Assert.Equal(expected, Strings.IsPalindrome(input));

    [Theory]
    [InlineData(19.99, "£19.99")]
    [InlineData(1, "£1.00")]
    [InlineData(0.5, "£0.50")]
    public void FormatPrice_FormatsWithPoundAndTwoDecimals(double amount, string expected)
        => Assert.Equal(expected, Strings.FormatPrice((decimal)amount));

    [Theory]
    [InlineData("42", 42)]
    [InlineData("0", 0)]
    [InlineData("-7", -7)]
    [InlineData("abc", -1)]
    [InlineData("", -1)]
    public void SafeParseInt_ReturnsMinusOneOnFailure(string input, int expected)
        => Assert.Equal(expected, Strings.SafeParseInt(input));
}
