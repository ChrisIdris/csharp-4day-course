using Fundamentals.Lessons;

namespace Fundamentals.Tests.Lessons;

// Guards for the teaching examples in Lessons/Structs.cs.
public class StructsTests
{
    // ── The Student struct itself ──────────────────────────────────

    [Fact]
    public void Student_ConstructorAssignsAllProperties()
    {
        Student s = new Student("Ada", "Lovelace", 36);
        Assert.Equal("Ada", s.FirstName);
        Assert.Equal("Lovelace", s.LastName);
        Assert.Equal(36, s.Age);
    }

    [Fact]
    public void Student_FullNameJoinsFirstAndLast()
        => Assert.Equal("Ada Lovelace", new Student("Ada", "Lovelace", 36).FullName());

    [Theory]
    [InlineData(36, true)]
    [InlineData(18, true)]
    [InlineData(17, false)]
    [InlineData(0, false)]
    public void Student_IsAdultChecksAge(int age, bool expected)
        => Assert.Equal(expected, new Student("X", "Y", age).IsAdult());

    [Fact]
    public void Student_PropertiesHaveSetters()
    {
        Student s = new Student("Ada", "Lovelace", 36);
        s.Age = 37;
        Assert.Equal(37, s.Age);
    }

    // ── Lesson E: instantiating and using ──────────────────────────

    [Fact]
    public void LessonE_MakeAdaReturnsExpectedStudent()
    {
        Student ada = Structs.MakeAda();
        Assert.Equal("Ada", ada.FirstName);
        Assert.Equal("Lovelace", ada.LastName);
        Assert.Equal(36, ada.Age);
    }

    [Fact]
    public void LessonE_AdaFullName()
        => Assert.Equal("Ada Lovelace", Structs.AdaFullName());

    [Fact]
    public void LessonE_AdaIsAdult()
        => Assert.True(Structs.AdaIsAdult());

    [Fact]
    public void LessonE_WithSetterChangesAge()
        => Assert.Equal(37, Structs.WithSetter().Age);

    // ── Lesson F: value semantics — THE point ──────────────────────

    [Fact]
    public void LessonF_AssignmentCopiesStruct_OriginalUnchanged()
    {
        (Student original, Student copy) = Structs.AssignmentCopiesStruct();
        Assert.Equal(36, original.Age);  // original untouched
        Assert.Equal(99, copy.Age);      // only the copy changed
    }

    [Fact]
    public void LessonF_PassingToMethodCopies_CallerUnchanged()
        => Assert.Equal(36, Structs.CallerAgeAfterBirthdayGotcha());

    [Fact]
    public void LessonF_FixedVersionReassignsReturnedValue()
        => Assert.Equal(46, Structs.CallerAgeAfterBirthdayFixed());
}
