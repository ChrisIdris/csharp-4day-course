namespace Fundamentals.Exercises;

// Theme: Strings (advanced) — exercises for you to implement.
// The teaching material (Lesson G: StringBuilder) is in Lessons/StringsAdvanced.cs.
// Tackle this only after finishing the core Strings exercises.
public static class StringsAdvanced
{
    // ADVANCED EXERCISE: ParseCsvRow
    //
    // Parse a single CSV row into its fields. Must handle:
    //
    //   1) Plain fields:        "one,two,three"          → ["one", "two", "three"]
    //   2) Quoted fields that   '"Hello, world",foo,bar' → ["Hello, world", "foo", "bar"]
    //      contain commas:        (a comma inside quotes is part of the field,
    //                              not a separator)
    //   3) Empty fields:        "a,,b"                   → ["a", "", "b"]
    //
    // Why this is a good StringBuilder exercise:
    //   - You can't just use row.Split(',') — that breaks case 2.
    //   - You'll build each field up CHARACTER BY CHARACTER while tracking
    //     whether you're currently inside quotes. StringBuilder is the right tool.
    //
    // Hint — state machine sketch:
    //   bool insideQuotes = false;
    //   var currentField = new StringBuilder();
    //   foreach (char c in row) {
    //       if (c == '"')                        → toggle insideQuotes, don't append
    //       else if (c == ',' && !insideQuotes)  → finish current field, start a new one
    //       else                                 → append c to currentField
    //   }
    //   // don't forget to add the last field after the loop ends!
    //
    // You don't need to handle escaped quotes ("" inside a quoted field).
    public static string[] ParseCsvRow(string row)
    {
        throw new NotImplementedException("TODO: walk the chars, track insideQuotes, build each field with StringBuilder");
    }
}
