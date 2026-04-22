namespace Fundamentals.Lessons;

// Theme: Lists — List<T> and your first taste of generics.
//
// Arrays (the previous theme) are fixed size. Most of the time you don't know
// up front how many items you'll have, so reaching for an array forces you to
// either over-allocate or re-allocate by hand. `List<T>` fixes that: it's a
// growable, strongly-typed sequence that's the default collection in real C#.
//
// This file contains the TEACHING EXAMPLES only. Exercises live in
// Exercises/Lists.cs.
public static class Lists
{
    // ─────────────────────────────────────────────────────────────
    // LESSON A: Generics in one minute — what does <T> mean?
    // ─────────────────────────────────────────────────────────────
    // `List<T>` is a GENERIC type. The `<T>` is a placeholder for the element
    // type that you fill in when you USE the list. `List<int>` means "a list
    // specialised to hold ints"; `List<string>` means "a list of strings".
    //
    // Closest analogues:
    //   TypeScript:  Array<number>, Array<string>
    //   Python:      list[int] (type hint only — Python lists are heterogeneous
    //                at runtime; C# lists are strictly one type)
    //
    // Why this matters: the compiler checks every Add/index/assignment against
    // the element type. `List<int>` rejects strings at compile time — you
    // never get a runtime "expected int, got string" surprise.

    public static List<int> OneInt()
    {
        // e.g. returns { 1, 2, 3 } — a List<int> with three ints
        return new List<int> { 1, 2, 3 };
    }

    public static List<string> OneString()
    {
        // e.g. returns { "a", "b", "c" } — a List<string>; can't hold ints
        return new List<string> { "a", "b", "c" };
    }

    // If you tried the following it wouldn't compile:
    //   var mixed = new List<int> { 1, "hi" };   // error: "hi" isn't an int

    // ─────────────────────────────────────────────────────────────
    // LESSON B: Why a list beats an array — it grows
    // ─────────────────────────────────────────────────────────────
    // The whole point of List<T>: Add, Insert, Remove at runtime. Arrays can't
    // do that — their size is locked in at creation. To "grow" an array you
    // have to allocate a bigger one and copy everything across by hand.

    // Showing the pain: to append one element to an array you'd write this.
    public static int[] ArrayCantGrow(int[] source, int extra)
    {
        // e.g. source = { 1, 2, 3 }, extra = 4 → returns { 1, 2, 3, 4 }
        //      — but look at the effort: allocate, copy, assign. Every. Time.
        int[] bigger = new int[source.Length + 1];
        for (int i = 0; i < source.Length; i++)
        {
            bigger[i] = source[i];
        }
        bigger[source.Length] = extra;
        return bigger;
    }

    // Same idea with a list — one method call, done:
    public static List<int> ListGrowsFreely()
    {
        // e.g. returns { 1, 2, 3, 4 } — built up by repeated Add
        List<int> items = new List<int>();
        items.Add(1);
        items.Add(2);
        items.Add(3);
        items.Add(4);
        return items;
    }

    // ─────────────────────────────────────────────────────────────
    // LESSON C: Four ways to create a list
    // ─────────────────────────────────────────────────────────────
    // Parallels the four array forms from the previous theme, with one
    // important gotcha around the constructor that takes a number.

    // 1) new List<int>() — empty list.
    public static List<int> EmptyList()
    {
        // e.g. returns { } — Count == 0
        return new List<int>();
    }

    // 2) new List<int> { ... } — initialiser with contents.
    public static List<int> WithInitialiser()
    {
        // e.g. returns { 10, 20, 30 } — Count == 3
        return new List<int> { 10, 20, 30 };
    }

    // 3) [ ... ] — collection expression (C# 12+). Works for lists too.
    public static List<int> CollectionExpression()
    {
        // e.g. returns { 10, 20, 30 }
        return [10, 20, 30];
    }

    // 4) new List<int>(capacity) — GOTCHA.
    // Passing a number to the constructor sets INITIAL CAPACITY (how much
    // room is reserved internally). It does NOT pre-fill with default values
    // the way `new int[5]` does. The Count is still 0.
    public static List<int> WithCapacityGotcha()
    {
        // e.g. returns { } — Count == 0, even though 5 slots are reserved.
        //      Contrast with `new int[5]` which gives you { 0, 0, 0, 0, 0 }.
        return new List<int>(5);
    }

    // ─────────────────────────────────────────────────────────────
    // LESSON D: .Count (not .Length), indexing, mutation
    // ─────────────────────────────────────────────────────────────
    // Small-but-annoying difference: arrays have `.Length`, lists have
    // `.Count`. Both are PROPERTIES (no parentheses) and both mean "how many
    // items". If you type `.Length` on a List<T> you'll get a compile error.
    //
    // Everything else feels familiar: zero-indexed, square-bracket access,
    // and slots are mutable.

    public static int HowMany(List<int> list)
    {
        // e.g. list = { 10, 20, 30 }; list.Count == 3
        return list.Count;
    }

    public static int FirstElement(List<int> list)
    {
        // e.g. list = { 10, 20, 30 }; list[0] == 10
        return list[0];
    }

    public static int LastElement(List<int> list)
    {
        // e.g. list = { 10, 20, 30 }; list[list.Count - 1] == 30
        return list[list.Count - 1];
    }

    // Lists are reference types (same as arrays). Mutations are visible to
    // the caller.
    public static void SetFirst(List<int> list, int value)
    {
        // e.g. list = { 1, 2, 3 }, value = 99; after call: list == { 99, 2, 3 }
        list[0] = value;
    }

    // Out of bounds still throws, but the exception type is different from
    // arrays: List<T> throws ArgumentOutOfRangeException, while arrays throw
    // IndexOutOfRangeException. Close cousins — easy to mix up in catch blocks.
    public static int AccessOutOfBounds(List<int> list)
    {
        // e.g. list has 3 items, list[100] → throws ArgumentOutOfRangeException
        return list[100];
    }

    // ─────────────────────────────────────────────────────────────
    // LESSON E: Add, Insert, Remove, RemoveAt, Clear
    // ─────────────────────────────────────────────────────────────
    // The list-specific API — this is what you came here for.

    public static List<int> AddToEnd(List<int> list, int value)
    {
        // e.g. list = { 1, 2 }, value = 3 → list becomes { 1, 2, 3 }
        list.Add(value);
        return list;
    }

    public static List<int> InsertAtFront(List<int> list, int value)
    {
        // e.g. list = { 2, 3 }, value = 1 → list becomes { 1, 2, 3 }
        list.Insert(0, value);
        return list;
    }

    // GOTCHA: on a List<int>, Remove(3) and RemoveAt(3) look alike but mean
    // very different things. Pay attention to which one you reach for.

    // Remove(x) removes the FIRST ELEMENT WHOSE VALUE EQUALS x.
    // Returns true if something was removed, false if x wasn't found.
    public static List<int> RemoveFirstMatch(List<int> list, int value)
    {
        // e.g. list = { 10, 20, 30, 20 }, value = 20 → list becomes { 10, 30, 20 }
        //      (only the FIRST 20 is removed)
        list.Remove(value);
        return list;
    }

    // RemoveAt(i) removes the element at INDEX i.
    public static List<int> RemoveAtIndex(List<int> list, int index)
    {
        // e.g. list = { 10, 20, 30, 40 }, index = 1 → list becomes { 10, 30, 40 }
        list.RemoveAt(index);
        return list;
    }

    // Clear empties the list completely — Count drops to 0.
    public static List<int> ClearAll(List<int> list)
    {
        // e.g. list = { 1, 2, 3 } → list becomes { }
        list.Clear();
        return list;
    }

    // ─────────────────────────────────────────────────────────────
    // LESSON F: Iteration — foreach and for both work
    // ─────────────────────────────────────────────────────────────
    // Same two patterns as arrays. Only difference in the `for` loop: use
    // `.Count` instead of `.Length`.

    public static int SumWithForeach(List<int> list)
    {
        // e.g. list = { 1, 2, 3, 4 }; returns 10
        int total = 0;
        foreach (int n in list)
        {
            total += n;
        }
        return total;
    }

    public static int SumWithFor(List<int> list)
    {
        // e.g. list = { 1, 2, 3, 4 }; returns 10 (same result, different style)
        int total = 0;
        for (int i = 0; i < list.Count; i++)
        {
            total += list[i];
        }
        return total;
    }

    // ─────────────────────────────────────────────────────────────
    // LESSON G: Sort / Reverse / IndexOf / Contains — instance methods now
    // ─────────────────────────────────────────────────────────────
    // On arrays these were STATIC methods on the Array class:
    //     Array.Sort(arr); Array.Reverse(arr); Array.IndexOf(arr, x);
    // On List<T> they're INSTANCE methods on the list itself:
    //     list.Sort(); list.Reverse(); list.IndexOf(x); list.Contains(x);
    //
    // Same in-place mutation behaviour as arrays: Sort and Reverse MUTATE
    // the list; they do not return a new copy.

    public static List<int> SortInPlace(List<int> list)
    {
        // e.g. list = { 3, 1, 2 }; after call: list == { 1, 2, 3 }
        list.Sort();
        return list;
    }

    public static List<int> ReverseInPlace(List<int> list)
    {
        // e.g. list = { 1, 2, 3 }; after call: list == { 3, 2, 1 }
        list.Reverse();
        return list;
    }

    public static int FindIndexOf(List<int> list, int value)
    {
        // e.g. list = { 10, 20, 30 }, value = 20 → returns 1
        //      list = { 10, 20, 30 }, value = 99 → returns -1 (not found)
        return list.IndexOf(value);
    }

    public static bool HasValue(List<int> list, int value)
    {
        // e.g. list = { 10, 20, 30 }, value = 20 → true
        //      list = { 10, 20, 30 }, value = 99 → false
        return list.Contains(value);
    }

    // ─────────────────────────────────────────────────────────────
    // LESSON H: Converting between arrays and lists
    // ─────────────────────────────────────────────────────────────
    // You'll need to bridge the two often — an API you don't control gives
    // you an array, but you want to grow it; or vice versa.

    public static List<int> ListFromArray(int[] source)
    {
        // e.g. source = { 1, 2, 3 } → returns a new List<int> { 1, 2, 3 }
        // The List<T> constructor accepts any sequence and copies it in.
        return new List<int>(source);
    }

    public static int[] ArrayFromList(List<int> source)
    {
        // e.g. source = { 1, 2, 3 } → returns a new int[] { 1, 2, 3 }
        return source.ToArray();
    }

    // ─────────────────────────────────────────────────────────────
    // Closing note
    // ─────────────────────────────────────────────────────────────
    // Rule of thumb:
    //   • Size fixed and known up front  → array (int[], string[], …)
    //   • Size changes at runtime        → List<T>
    // In practice, most real C# code reaches for List<T> by default.
}
