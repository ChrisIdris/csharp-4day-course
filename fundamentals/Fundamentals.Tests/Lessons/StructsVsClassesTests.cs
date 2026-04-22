using Fundamentals.Lessons;

namespace Fundamentals.Tests.Lessons;

// Guards for the teaching examples in Lessons/StructsVsClasses.cs.
// The key property of these tests: the STRUCT case and the CLASS case
// have identical setup code and produce opposite outcomes.
public class StructsVsClassesTests
{
    // ── Experiment 1: assignment ───────────────────────────────────

    [Fact]
    public void StructAssignmentCopies_OriginalUntouched()
    {
        (PointStruct a, PointStruct b) = StructsVsClasses.StructAssignmentCopies();
        Assert.Equal(1, a.X);   // `a` is the independent copy — untouched
        Assert.Equal(99, b.X);  // only `b` was mutated
    }

    [Fact]
    public void ClassAssignmentShares_BothNamesSeeTheMutation()
    {
        (PointClass a, PointClass b) = StructsVsClasses.ClassAssignmentShares();
        Assert.Equal(99, a.X);  // `a` and `b` are two names for the SAME object
        Assert.Equal(99, b.X);
        Assert.Same(a, b);      // prove it: they really are the one-and-only object
    }

    // ── Experiment 2: passing to a method ──────────────────────────

    [Fact]
    public void StructPassingCopies_CallerUntouched()
        => Assert.Equal(1, StructsVsClasses.StructPassingCopies().X);

    [Fact]
    public void ClassPassingShares_CallerMutated()
        => Assert.Equal(99, StructsVsClasses.ClassPassingShares().X);

    // ── The whole point, stated as a single assertion pair ─────────
    // Same experiment, struct vs class, opposite outcomes.

    [Fact]
    public void OneKeywordChanged_OppositeOutcome_Assignment()
    {
        int structOriginalX = StructsVsClasses.StructAssignmentCopies().a.X;
        int classOriginalX  = StructsVsClasses.ClassAssignmentShares().a.X;
        Assert.NotEqual(structOriginalX, classOriginalX);
        Assert.Equal(1, structOriginalX);
        Assert.Equal(99, classOriginalX);
    }

    [Fact]
    public void OneKeywordChanged_OppositeOutcome_Passing()
    {
        int structOriginalX = StructsVsClasses.StructPassingCopies().X;
        int classOriginalX  = StructsVsClasses.ClassPassingShares().X;
        Assert.NotEqual(structOriginalX, classOriginalX);
        Assert.Equal(1, structOriginalX);
        Assert.Equal(99, classOriginalX);
    }
}
