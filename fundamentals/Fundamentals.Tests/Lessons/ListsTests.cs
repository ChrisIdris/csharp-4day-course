using Fundamentals.Lessons;

namespace Fundamentals.Tests.Lessons;

// Guards for the teaching examples in Lessons/Lists.cs.
public class ListsTests
{
    // ── Lesson A: generics intro ──

    [Fact]
    public void LessonA_OneIntHasThreeInts()
        => Assert.Equal(new[] { 1, 2, 3 }, Lists.OneInt());

    [Fact]
    public void LessonA_OneStringHasThreeStrings()
        => Assert.Equal(new[] { "a", "b", "c" }, Lists.OneString());

    // ── Lesson B: growable ──

    [Fact]
    public void LessonB_ArrayCantGrowAppendsElement()
        => Assert.Equal(new[] { 1, 2, 3, 4 }, Lists.ArrayCantGrow(new[] { 1, 2, 3 }, 4));

    [Fact]
    public void LessonB_ListGrowsFreelyBuildsFourElements()
        => Assert.Equal(new[] { 1, 2, 3, 4 }, Lists.ListGrowsFreely());

    // ── Lesson C: four creation forms ──

    [Fact]
    public void LessonC_EmptyListHasCountZero()
        => Assert.Empty(Lists.EmptyList());

    [Fact]
    public void LessonC_InitialiserAndCollectionExpressionAgree()
        => Assert.Equal(Lists.WithInitialiser(), Lists.CollectionExpression());

    [Fact]
    public void LessonC_CapacityDoesNotPreFill()
    {
        // The point of the gotcha method: capacity argument gives an empty list,
        // NOT 5 default-filled slots like `new int[5]` would.
        List<int> list = Lists.WithCapacityGotcha();
        Assert.Empty(list);
    }

    // ── Lesson D: Count, indexing, mutation, out of bounds ──

    [Fact]
    public void LessonD_HowManyAndElementAccessors()
    {
        List<int> list = new List<int> { 10, 20, 30 };
        Assert.Equal(3, Lists.HowMany(list));
        Assert.Equal(10, Lists.FirstElement(list));
        Assert.Equal(30, Lists.LastElement(list));
    }

    [Fact]
    public void LessonD_SetFirstMutatesCallersList()
    {
        List<int> list = new List<int> { 1, 2, 3 };
        Lists.SetFirst(list, 99);
        Assert.Equal(99, list[0]);
        Assert.Equal(2, list[1]);
    }

    [Fact]
    public void LessonD_AccessOutOfBoundsThrows()
        => Assert.Throws<ArgumentOutOfRangeException>(
            () => Lists.AccessOutOfBounds(new List<int> { 1, 2, 3 }));

    // ── Lesson E: Add / Insert / Remove / RemoveAt / Clear ──

    [Fact]
    public void LessonE_AddToEndAppends()
        => Assert.Equal(new[] { 1, 2, 3 }, Lists.AddToEnd(new List<int> { 1, 2 }, 3));

    [Fact]
    public void LessonE_InsertAtFrontPrepends()
        => Assert.Equal(new[] { 1, 2, 3 }, Lists.InsertAtFront(new List<int> { 2, 3 }, 1));

    [Fact]
    public void LessonE_RemoveFirstMatchRemovesOnlyTheFirstOccurrence()
        => Assert.Equal(
            new[] { 10, 30, 20 },
            Lists.RemoveFirstMatch(new List<int> { 10, 20, 30, 20 }, 20));

    [Fact]
    public void LessonE_RemoveAtIndexRemovesByIndex()
        => Assert.Equal(
            new[] { 10, 30, 40 },
            Lists.RemoveAtIndex(new List<int> { 10, 20, 30, 40 }, 1));

    [Fact]
    public void LessonE_ClearAllEmptiesTheList()
        => Assert.Empty(Lists.ClearAll(new List<int> { 1, 2, 3 }));

    // ── Lesson F: iteration ──

    [Fact]
    public void LessonF_ForeachAndForProduceSameSum()
    {
        List<int> list = new List<int> { 1, 2, 3, 4, 5 };
        Assert.Equal(Lists.SumWithForeach(list), Lists.SumWithFor(list));
        Assert.Equal(15, Lists.SumWithForeach(list));
    }

    // ── Lesson G: instance utility methods ──

    [Fact]
    public void LessonG_SortInPlaceMutatesTheInput()
    {
        List<int> list = new List<int> { 3, 1, 2 };
        Lists.SortInPlace(list);
        Assert.Equal(new[] { 1, 2, 3 }, list);
    }

    [Fact]
    public void LessonG_ReverseInPlaceMutatesTheInput()
    {
        List<int> list = new List<int> { 1, 2, 3 };
        Lists.ReverseInPlace(list);
        Assert.Equal(new[] { 3, 2, 1 }, list);
    }

    [Fact]
    public void LessonG_FindIndexOfReturnsMinusOneWhenNotFound()
    {
        Assert.Equal(1, Lists.FindIndexOf(new List<int> { 10, 20, 30 }, 20));
        Assert.Equal(-1, Lists.FindIndexOf(new List<int> { 10, 20, 30 }, 99));
    }

    [Fact]
    public void LessonG_HasValueReturnsContains()
    {
        Assert.True(Lists.HasValue(new List<int> { 10, 20, 30 }, 20));
        Assert.False(Lists.HasValue(new List<int> { 10, 20, 30 }, 99));
    }

    // ── Lesson H: conversion ──

    [Fact]
    public void LessonH_ListFromArrayCopiesContents()
        => Assert.Equal(new[] { 1, 2, 3 }, Lists.ListFromArray(new[] { 1, 2, 3 }));

    [Fact]
    public void LessonH_ArrayFromListCopiesContents()
        => Assert.Equal(new[] { 1, 2, 3 }, Lists.ArrayFromList(new List<int> { 1, 2, 3 }));
}
