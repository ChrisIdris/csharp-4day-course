namespace Fundamentals.Exercises;

// Theme: Strings — exercises for you to implement.
// The teaching material for this theme is in Lessons/Strings.cs — read that first.
public static class Strings
{
    // EXERCISE 1: Shout
    // Return the message in UPPERCASE with "!!!" at the end.
    // Example: Shout("hello") → "HELLO!!!"
    public static string Shout(string message)
    {
        throw new NotImplementedException("TODO: uppercase the message and append !!!");
    }

    // EXERCISE 2: CountVowels
    // Return the number of vowels (a, e, i, o, u) in the text. Case-insensitive.
    // Example: CountVowels("Hello World") → 3
    // Hint: iterate with `foreach (char c in text) { ... }` and compare to a set/list
    //       of vowels. Lowercase the char first with char.ToLower(c).
    public static int CountVowels(string text)
    {
        throw new NotImplementedException("TODO: loop over each char, count vowels");
    }

    // EXERCISE 3: IsPalindrome
    // Return true if the word reads the same forwards and backwards, ignoring case.
    // Example: IsPalindrome("Racecar") → true, IsPalindrome("hello") → false
    // Hint: compare word against a reversed version. You can reverse with
    //       new string(word.Reverse().ToArray()) — Reverse comes from LINQ (already using'd).
    public static bool IsPalindrome(string word)
    {
        throw new NotImplementedException("TODO: compare word to its reverse, case-insensitive");
    }

    // EXERCISE 4: FormatPrice
    // Return a string formatted as "£19.99" for the given price.
    // Example: FormatPrice(19.99m) → "£19.99", FormatPrice(1m) → "£1.00"
    // Hint: interpolation with a format specifier: $"£{amount:F2}" — F2 means
    //       fixed-point with 2 decimals. (We hardcode £ rather than rely on :C so
    //       the output is the same regardless of system locale.)
    public static string FormatPrice(decimal amount)
    {
        throw new NotImplementedException("TODO: prepend £ and format to 2 decimals");
    }

    // EXERCISE 5: SafeParseInt
    // Return the parsed integer if `input` is a valid integer; otherwise return -1.
    // Example: SafeParseInt("42") → 42, SafeParseInt("abc") → -1
    // Hint: use int.TryParse — don't let a FormatException escape.
    public static int SafeParseInt(string input)
    {
        throw new NotImplementedException("TODO: use int.TryParse and return -1 on failure");
    }
}
