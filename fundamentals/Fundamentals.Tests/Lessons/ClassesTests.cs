using Fundamentals.Lessons;

namespace Fundamentals.Tests.Lessons;

// Guards for the teaching examples in Lessons/Classes.cs.
public class ClassesTests
{
    // ── The Course class itself ────────────────────────────────────

    [Fact]
    public void Course_ConstructorSetsNameAndInitialisesEmptyList()
    {
        Course c = new Course("Physics 101");
        Assert.Equal("Physics 101", c.Name);
        Assert.NotNull(c.Students);
        Assert.Empty(c.Students);
    }

    [Fact]
    public void Course_EnrollAppendsStudent()
    {
        Course c = new Course("Physics 101");
        c.Enroll(new Student("Ada", "Lovelace", 36));
        Assert.Equal(1, c.Count());
        Assert.Equal("Ada", c.Students[0].FirstName);
    }

    [Fact]
    public void Course_RemoveReturnsTrueWhenFound()
    {
        Course c = new Course("Physics 101");
        c.Enroll(new Student("Ada", "Lovelace", 36));
        c.Enroll(new Student("Alan", "Turing", 24));
        Assert.True(c.Remove("Alan", "Turing"));
        Assert.Equal(1, c.Count());
        Assert.Equal("Ada", c.Students[0].FirstName);
    }

    [Fact]
    public void Course_RemoveReturnsFalseWhenMissing()
    {
        Course c = new Course("Physics 101");
        c.Enroll(new Student("Ada", "Lovelace", 36));
        Assert.False(c.Remove("Nobody", "Here"));
        Assert.Equal(1, c.Count());
    }

    [Fact]
    public void Course_StudentNamesReturnsFullNames()
    {
        Course c = new Course("Physics 101");
        c.Enroll(new Student("Ada", "Lovelace", 36));
        c.Enroll(new Student("Alan", "Turing", 24));
        Assert.Equal(new[] { "Ada Lovelace", "Alan Turing" }, c.StudentNames());
    }

    // ── Lesson-level demos ─────────────────────────────────────────

    [Fact]
    public void LessonE_PhysicsCountIsTwo()
        => Assert.Equal(2, Classes.PhysicsCount());

    [Fact]
    public void LessonE_PhysicsNamesAreBoth()
        => Assert.Equal(new[] { "Ada Lovelace", "Alan Turing" }, Classes.PhysicsNames());

    [Fact]
    public void LessonE_RemoveTuringReturnsTrue()
        => Assert.True(Classes.RemoveTuring());

    // ── Lesson E big-one: reference semantics ──────────────────────

    [Fact]
    public void LessonE_AssignmentSharesReference_BothVariablesSeeTheMutation()
    {
        (int c1Count, int c2Count) = Classes.AssignmentSharesReference();
        Assert.Equal(1, c1Count);  // same object — both "see" the enrolled student
        Assert.Equal(1, c2Count);
    }

    [Fact]
    public void LessonE_PassingSharesReference_CallerSeesTheMutation()
    {
        // Direct contrast with CallerAgeAfterBirthdayGotcha in StructsTests,
        // where the caller saw NO change. Here the caller DOES see the change.
        Assert.Equal(1, Classes.CallerCountAfterEnroll());
    }
}
