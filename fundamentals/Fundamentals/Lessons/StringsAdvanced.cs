using System.Text;

namespace Fundamentals.Lessons;

// Theme: Strings (advanced) — covers StringBuilder and why it matters.
// Tackle this AFTER you've finished the core Strings lesson. The matching
// exercise (ParseCsvRow) lives in Exercises/StringsAdvanced.cs.
public static class StringsAdvanced
{
    // ─────────────────────────────────────────────────────────────
    // LESSON G: StringBuilder — building strings efficiently
    // ─────────────────────────────────────────────────────────────
    // Strings are IMMUTABLE. Every time you write `result += part`, C# allocates
    // a brand-new string containing the concatenation, then throws the old one away.
    //
    //   Iteration 1: allocate "a"           (1 char copied)
    //   Iteration 2: allocate "ab"          (2 chars copied)
    //   Iteration 3: allocate "abc"         (3 chars copied)
    //   ...
    //   Iteration N: allocate N-char string (N chars copied)
    //
    // That's O(N²) total work for N items. Fine at 10, painful at 10,000.
    //
    // `StringBuilder` is a MUTABLE buffer. .Append doesn't allocate a new string
    // each call — it writes into an internal buffer that grows as needed.
    // Only .ToString() at the end produces a final string.

    // The naive version. Works, but inefficient for large inputs.
    public static string JoinWithConcatenation(string[] parts)
    {
        string result = "";
        foreach (string part in parts)
        {
            result += part; // new string allocated every iteration
        }
        return result;
    }

    // The idiomatic fix for loops with custom logic.
    public static string JoinWithStringBuilder(string[] parts)
    {
        var sb = new StringBuilder();
        foreach (string part in parts)
        {
            sb.Append(part); // mutates the buffer — no new string per iteration
        }
        return sb.ToString(); // single allocation at the end
    }

    // If all you're doing is concatenating a collection with an optional separator,
    // `string.Join` is shorter, clearer, and already efficient under the hood.
    // Reach for StringBuilder only when the logic is more involved than "just join them".
    public static string JoinWithStringJoin(string[] parts)
    {
        return string.Join("", parts);
    }
}
