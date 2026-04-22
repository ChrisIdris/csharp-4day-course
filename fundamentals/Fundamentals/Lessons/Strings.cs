namespace Fundamentals.Lessons;

// Theme: Strings — C#-specific syntax and idioms.
// Assumes you already know strings from JavaScript/Python — we focus on what's
// different in C#.
//
// This file contains the TEACHING EXAMPLES only. Your work lives in
// Exercises/Strings.cs.
public static class Strings
{
    // ─────────────────────────────────────────────────────────────
    // LESSON A: `char` vs `string` — single quotes mean something different
    // ─────────────────────────────────────────────────────────────
    // In C#, single and double quotes produce DIFFERENT types:
    //   'a'  is a `char`    — exactly ONE character, a primitive value type
    //   "a"  is a `string`  — a sequence of characters (may be empty or longer)
    //
    // Unlike JavaScript where 'a' and "a" both give you a string of length 1,
    // C# makes you pick the right type. This catches beginners out.
    //
    // Indexing a string returns a CHAR, not a 1-length string:
    //   "hello"[0]  →  'h'   (char, not "h")

    public static char FirstLetter(string word)
    {
        return word[0]; // returns a char
    }

    // GOTCHA: adding two chars does INTEGER arithmetic on their character codes.
    // 'a' is code 97, 'b' is code 98, so 'a' + 'b' == 195 (an int), NOT "ab".
    // To concatenate them as text, use strings: "a" + "b" == "ab".
    public static int AddCharsAsNumbers(char a, char b)
    {
        return a + b; // char + char → int
    }

    public static string AddStringsAsText(string a, string b)
    {
        return a + b; // string + string → string
    }

    // ─────────────────────────────────────────────────────────────
    // LESSON B: Three ways to write a string literal
    // ─────────────────────────────────────────────────────────────

    // 1) REGULAR string — escape sequences like \n, \t, \", \\  (just like JS).
    public static string RegularLiteral()
    {
        return "Line one\nLine two\tTabbed\nShe said \"hi\"";
    }

    // 2) VERBATIM string — prefix with @  — NO escape sequences.
    //
    //    First, the problem: in a REGULAR string, certain characters have
    //    special meaning and must be ESCAPED with a backslash:
    //
    //        \\   a literal backslash
    //        \"   a literal double-quote
    //        \n   newline
    //        \t   tab
    //        \0   null character
    //
    //    So a Windows path like C:\Users\dogezen\docs\file.txt has to be
    //    written with each single backslash doubled up — awkward and error-prone:
    public static string WindowsPath_RegularString()
    {
        return "C:\\Users\\dogezen\\docs\\file.txt"; // every \ must be \\
    }

    //    Verbatim strings fix this. Prefix the literal with @ and the compiler
    //    treats backslashes (and most other escapes) as literal text. Same
    //    resulting value, much easier to read. Great for Windows paths and regex.
    public static string WindowsPath_VerbatimString()
    {
        return @"C:\Users\dogezen\docs\file.txt"; // backslashes are literal
    }

    //    Inside a verbatim string, the ONE thing you still need to escape is a
    //    double-quote — and you do it by DOUBLING it (not with backslash):
    public static string VerbatimWithQuotes()
    {
        return @"She said ""hello"" loudly.";
    }

    // 3) RAW string literal (C# 11+) — three-or-more double quotes "".."".
    //    No escaping at all. Can span multiple lines. Like Python's """...""".
    //    You can use " inside freely — the opening "fence" just needs to be
    //    at least one longer than any run of " inside.
    public static string RawLiteral()
    {
        return """
            {
              "name": "Dogezen",
              "role": "instructor"
            }
            """;
    }

    // INTERPOLATION — prefix with $ — like a JS template literal or Python f-string.
    // Combine prefixes: $@"..." (interpolated verbatim), $"""...""" (interpolated raw).
    public static string Interpolated(string name, int age)
    {
        return $"Hello {name}, you are {age} years old.";
    }

    // ─────────────────────────────────────────────────────────────
    // LESSON C: Strings are immutable — methods return NEW strings
    // ─────────────────────────────────────────────────────────────
    // Calling a "modification" method like .ToUpper() or .Replace() does NOT
    // change the original string — it returns a new one. If you don't
    // capture the result, nothing visible happens. Classic beginner trap.

    // Gotcha version — calling ToUpper and ignoring the result:
    public static string ImmutabilityGotcha(string input)
    {
        input.ToUpper(); // return value thrown away — `input` is unchanged
        return input;    // returns the original, lowercase or whatever it was
    }

    // Fix — assign the result back:
    public static string ImmutabilityFixed(string input)
    {
        input = input.ToUpper(); // capture the NEW string
        return input;
    }

    // Common methods — note .Length is a PROPERTY (no parentheses), just like
    // in JavaScript. Everything else is a method and needs () to invoke it.

    public static int LengthOf(string s)
    {
        // e.g. s = "hello"; s.Length == 5
        return s.Length;
    }

    public static string Uppercase(string s)
    {
        // e.g. s = "hello"; s.ToUpper() == "HELLO"
        return s.ToUpper();
    }

    public static string Trimmed(string s)
    {
        // e.g. s = "  hi  "; s.Trim() == "hi"   (strips leading/trailing whitespace)
        return s.Trim();
    }

    public static string Swap(string s)
    {
        // e.g. s = "the cat sat"; s.Replace("cat", "dog") == "the dog sat"
        return s.Replace("cat", "dog");
    }

    public static bool Mentions(string haystack, string needle)
    {
        // e.g. haystack = "hello world", needle = "world"; Contains → true
        return haystack.Contains(needle);
    }

    public static string[] SplitWords(string sentence)
    {
        // e.g. sentence = "one two three"; Split(' ') == ["one", "two", "three"]
        return sentence.Split(' ');
    }

    public static string JoinWords(string[] words)
    {
        // e.g. words = ["one", "two", "three"]; string.Join(" ", words) == "one two three"
        return string.Join(" ", words);
    }

    // ─────────────────────────────────────────────────────────────
    // LESSON D: Comparing strings
    // ─────────────────────────────────────────────────────────────
    // In C#, `==` compares STRING VALUES (not references — unlike Java).
    // So "hello" == "hello" is true, just like in JS.
    //
    // `==` is case-sensitive. For case-insensitive, use:
    //   string.Equals(a, b, StringComparison.OrdinalIgnoreCase)

    public static bool SameExact(string a, string b)
    {
        return a == b; // case-sensitive
    }

    public static bool SameIgnoringCase(string a, string b)
    {
        return string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
    }

    // `null` and "" (empty string) are different values. Both can trip you up
    // when checking "is there anything here?". The built-in helpers cover both.
    public static bool HasContent(string? s)
    {
        // IsNullOrEmpty returns true for null or "".
        // IsNullOrWhiteSpace ALSO returns true for "   " (only whitespace).
        return !string.IsNullOrWhiteSpace(s);
    }

    // ─────────────────────────────────────────────────────────────
    // LESSON E: Parsing strings to numbers — Parse vs TryParse
    // ─────────────────────────────────────────────────────────────
    // int.Parse("42") returns 42. But int.Parse("abc") THROWS a FormatException.
    // If the input might not be a valid number (user input, file data, etc.),
    // use int.TryParse — it returns a bool and gives you the value via an
    // `out` parameter. No exception, no crash.

    public static int ParseOrCrash(string input)
    {
        return int.Parse(input); // throws on bad input
    }

    public static bool TryParseSafely(string input, out int value)
    {
        // `out int value` means: this method will assign a value to the caller's variable.
        // Returns true if parsing succeeded, false otherwise.
        return int.TryParse(input, out value);
    }

    // ─────────────────────────────────────────────────────────────
    // LESSON F: Format specifiers inside interpolation
    // ─────────────────────────────────────────────────────────────
    // Inside $"...{ value : FORMAT }...", FORMAT controls how the value is rendered.
    // Common specifiers:
    //   :C    currency (culture-aware: £ in en-GB, $ in en-US)
    //   :N2   number with 2 decimal places and thousands separators
    //   :F2   fixed-point, 2 decimal places, no thousands separator
    //   :D5   integer zero-padded to 5 digits
    //   :P1   percentage with 1 decimal place
    //
    // Note: some of these are culture-sensitive. If you need the SAME output
    // everywhere, pass a specific CultureInfo — we'll skip that here.

    public static string PriceAsCurrency(decimal amount)
    {
        // e.g. amount = 19.99m; $"{amount:C}" == "£19.99"  (with en-GB culture)
        return $"{amount:C}";
    }

    public static string RoundedToTwo(double value)
    {
        // e.g. value = 3.14159; $"{value:N2}" == "3.14"
        return $"{value:N2}";
    }

    public static string PaddedInteger(int n)
    {
        // e.g. n = 42; $"{n:D5}" == "00042"
        return $"{n:D5}";
    }
}
