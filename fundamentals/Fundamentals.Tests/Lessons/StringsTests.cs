using System.Globalization;
using Fundamentals.Lessons;

namespace Fundamentals.Tests.Lessons;

// Guards for the teaching examples in Lessons/Strings.cs.
// These should always pass — they protect the lesson content from accidental "fixes".
public class StringsTests
{
    public StringsTests()
    {
        // Pin culture so format-specifier tests (like :C) are deterministic
        // regardless of the host's locale.
        CultureInfo.CurrentCulture = new CultureInfo("en-GB");
    }

    [Fact]
    public void LessonA_StringIndexingReturnsChar()
        => Assert.Equal('h', Strings.FirstLetter("hello"));

    [Fact]
    public void LessonA_AddingCharsIsIntegerArithmetic()
        => Assert.Equal('a' + 'b', Strings.AddCharsAsNumbers('a', 'b'));

    [Fact]
    public void LessonA_AddingStringsConcatenates()
        => Assert.Equal("ab", Strings.AddStringsAsText("a", "b"));

    [Fact]
    public void LessonB_RegularStringEscapesBackslashes()
        => Assert.Equal(@"C:\Users\dogezen\docs\file.txt", Strings.WindowsPath_RegularString());

    [Fact]
    public void LessonB_VerbatimPreservesBackslashes()
        => Assert.Equal(@"C:\Users\dogezen\docs\file.txt", Strings.WindowsPath_VerbatimString());

    [Fact]
    public void LessonB_RegularAndVerbatimProduceTheSameValue()
        => Assert.Equal(Strings.WindowsPath_RegularString(), Strings.WindowsPath_VerbatimString());

    [Fact]
    public void LessonB_VerbatimQuotesAreDoubled()
        => Assert.Equal("She said \"hello\" loudly.", Strings.VerbatimWithQuotes());

    [Fact]
    public void LessonC_ImmutabilityGotchaDoesNotUppercase()
        => Assert.Equal("hello", Strings.ImmutabilityGotcha("hello"));

    [Fact]
    public void LessonC_ImmutabilityFixedUppercases()
        => Assert.Equal("HELLO", Strings.ImmutabilityFixed("hello"));

    [Fact]
    public void LessonD_EqualsIsCaseSensitive()
    {
        Assert.True(Strings.SameExact("hello", "hello"));
        Assert.False(Strings.SameExact("Hello", "hello"));
    }

    [Fact]
    public void LessonD_EqualsIgnoreCaseWorks()
    {
        Assert.True(Strings.SameIgnoringCase("Hello", "hello"));
        Assert.False(Strings.SameIgnoringCase("Hello", "world"));
    }

    [Theory]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData("   ", false)]
    [InlineData("hi", true)]
    public void LessonD_HasContentCoversNullEmptyWhitespace(string? input, bool expected)
        => Assert.Equal(expected, Strings.HasContent(input));

    [Fact]
    public void LessonE_ParseThrowsOnBadInput()
        => Assert.Throws<FormatException>(() => Strings.ParseOrCrash("abc"));

    [Fact]
    public void LessonE_TryParseReturnsFalseOnBadInput()
    {
        bool ok = Strings.TryParseSafely("abc", out int _);
        Assert.False(ok);
    }

    [Fact]
    public void LessonE_TryParseReturnsValueOnGoodInput()
    {
        bool ok = Strings.TryParseSafely("42", out int value);
        Assert.True(ok);
        Assert.Equal(42, value);
    }

    [Fact]
    public void LessonF_PaddedIntegerPadsWithZeros()
        => Assert.Equal("00042", Strings.PaddedInteger(42));
}
