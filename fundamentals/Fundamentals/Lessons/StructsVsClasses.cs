namespace Fundamentals.Lessons;

// Theme: Structs vs Classes — the sharpest possible contrast.
//
// This file defines TWO types with identical shape (same properties, same
// constructor, same body). The ONLY difference is one keyword on the type
// declaration:
//
//     public struct PointStruct { ... }
//     public class  PointClass  { ... }
//
// We then run the same two experiments on both (assignment, passing to a
// method) and see OPPOSITE outcomes. One keyword changed; the runtime
// behaviour flips. That's the whole lesson.
//
// Read this file AFTER Structs.cs (Lesson F) and Classes.cs (Lesson E) —
// it's the synthesis that pulls them side by side.

// ─────────────────────────────────────────────────────────────
// Two types, one letter apart.
// ─────────────────────────────────────────────────────────────
public struct PointStruct
{
    public int X { get; set; }
    public int Y { get; set; }

    public PointStruct(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public class PointClass
{
    public int X { get; set; }
    public int Y { get; set; }

    public PointClass(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public static class StructsVsClasses
{
    // ═════════════════════════════════════════════════════════════
    // EXPERIMENT 1 — assignment
    //
    // The same four lines of code in both methods. The only thing that
    // differs is whether the variable's TYPE is a struct or a class.
    //
    //     var a = new Point(1, 2);
    //     var b = a;
    //     b.X = 99;
    //     return (a, b);
    //
    //   STRUCT:   a.X == 1,   b.X == 99   ← `b = a` made a COPY
    //   CLASS:    a.X == 99,  b.X == 99   ← `b = a` shared the SAME object
    // ═════════════════════════════════════════════════════════════

    public static (PointStruct a, PointStruct b) StructAssignmentCopies()
    {
        // e.g. a.X == 1, b.X == 99 — a is untouched because b is a copy
        PointStruct a = new PointStruct(1, 2);
        PointStruct b = a;   // COPY — b is a wholly separate PointStruct
        b.X = 99;            // mutates b only
        return (a, b);
    }

    public static (PointClass a, PointClass b) ClassAssignmentShares()
    {
        // e.g. a.X == 99, b.X == 99 — `a` and `b` are two names for ONE object
        PointClass a = new PointClass(1, 2);
        PointClass b = a;    // SAME reference — no copy
        b.X = 99;            // the one-and-only object's X is now 99
        return (a, b);
    }

    // ═════════════════════════════════════════════════════════════
    // EXPERIMENT 2 — passing to a method
    //
    // Same shape: build a point, pass it to a helper that sets X to 99,
    // then look at the original.
    //
    //   STRUCT:   original.X == 1    ← helper got a COPY; caller untouched
    //   CLASS:    original.X == 99   ← helper got the SAME reference; caller mutated
    // ═════════════════════════════════════════════════════════════

    public static void MutatePointStruct(PointStruct p)
    {
        p.X = 99; // modifies the local copy; caller won't see it
    }

    public static PointStruct StructPassingCopies()
    {
        // e.g. returns PointStruct with X == 1 — MutatePointStruct mutated a COPY
        PointStruct a = new PointStruct(1, 2);
        MutatePointStruct(a);
        return a;
    }

    public static void MutatePointClass(PointClass p)
    {
        p.X = 99; // modifies the SAME object the caller holds; they will see it
    }

    public static PointClass ClassPassingShares()
    {
        // e.g. returns PointClass with X == 99 — MutatePointClass mutated the caller's object
        PointClass a = new PointClass(1, 2);
        MutatePointClass(a);
        return a;
    }

    // ─────────────────────────────────────────────────────────────
    // The takeaway
    // ─────────────────────────────────────────────────────────────
    // Identical type shape. Identical operation code. One keyword flipped
    // (`struct` → `class`). Opposite outcomes — every single time.
    //
    // When you're picking between `struct` and `class` in your own code,
    // this is the question to ask first:
    //
    //    "Do I want assignment and method-passing to SHARE or COPY?"
    //
    // If in doubt, use a class — classes are the default in C# for a reason.
    // Reach for a struct only for small, simple, value-like data (coordinates,
    // money, date ranges, colours).
}
