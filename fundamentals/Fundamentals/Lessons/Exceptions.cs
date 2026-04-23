namespace Fundamentals.Lessons;

// Theme: Exceptions — throwing and catching errors in C#.
//
// Closest analogues from your background:
//   Python:     raise / try / except / finally
//   JavaScript: throw / try / catch / finally
//
// The shape is near-identical. Three C#-specific points worth noting:
//   1) `catch` declares the exception TYPE you want to handle — you pick
//      which kind of failure you can cope with at this spot.
//   2) You don't have to put a variable name in the catch — `catch (FormatException)`
//      is valid if you just want to react to the type, not inspect it.
//   3) C# has a large library of pre-built exception types. Prefer picking
//      the most specific built-in that fits the problem (ArgumentException,
//      InvalidOperationException, etc.) over inventing new ones.
//
// This file contains the TEACHING EXAMPLES only. Your work lives in
// Exercises/Exceptions.cs.
public static class Exceptions
{
    // ─────────────────────────────────────────────────────────────
    // LESSON A: try / catch — the basic shape
    // ─────────────────────────────────────────────────────────────
    // `int.Parse("abc")` throws `FormatException` at runtime (we saw this
    // in the Strings lesson). A try/catch lets us RECOVER from that rather
    // than crash. Control jumps from the throwing line straight into the
    // matching catch block; anything after the `throw` inside `try` is
    // skipped.

    public static int ParseOrFallback(string text, int fallback)
    {
        // e.g. ParseOrFallback("42", -1)  == 42
        //      ParseOrFallback("abc", -1) == -1  ← Parse threw, catch ran
        try
        {
            return int.Parse(text);
        }
        catch (FormatException)
        {
            // We don't need the exception object, so no variable name.
            return fallback;
        }
    }

    // ─────────────────────────────────────────────────────────────
    // LESSON B: throw — raising an exception yourself
    // ─────────────────────────────────────────────────────────────
    // `throw new SomeException("message")` is C#'s version of `raise` / `throw`.
    // When a method is asked to do something impossible, the right move is
    // usually to throw rather than to return a magic "error" value. Callers
    // can then choose: either catch the exception here and recover, or let
    // it bubble up.
    //
    // Built-in exceptions you'll reach for most:
    //   ArgumentException        — caller passed something invalid
    //   ArgumentNullException    — specifically: a required argument was null
    //   InvalidOperationException — the object isn't in a state where this call makes sense
    //                              (e.g. withdrawing more money than the balance)

    public static int Divide(int a, int b)
    {
        // e.g. Divide(10, 2) == 5
        //      Divide(10, 0) → throws DivideByZeroException
        if (b == 0)
        {
            // nameof(b) gives the string "b" — refactor-safe: if you rename
            // the parameter, the string updates automatically.
            throw new ArgumentException("Cannot divide by zero", nameof(b));
        }
        return a / b;
    }

    // ─────────────────────────────────────────────────────────────
    // LESSON C: Catching what you threw
    // ─────────────────────────────────────────────────────────────
    // When a caller knows an operation might fail, they can wrap it in a
    // try/catch and react specifically to the exception type they expect.
    // Inside the catch block, `ex.Message` gives you the message you passed
    // when throwing — useful for logging or user-facing output.

    public static string SafeDivideAsMessage(int a, int b)
    {
        // e.g. SafeDivideAsMessage(10, 2) == "5"
        //      SafeDivideAsMessage(10, 0) == "error: Cannot divide by zero (Parameter 'b')"
        try
        {
            return Divide(a, b).ToString();
        }
        catch (ArgumentException ex)
        {
            return $"error: {ex.Message}";
        }
    }

    // ─────────────────────────────────────────────────────────────
    // LESSON D: Catch the SPECIFIC exception type, not `Exception`
    // ─────────────────────────────────────────────────────────────
    // A bare `catch { ... }` (or `catch (Exception) { ... }`) catches
    // EVERYTHING — not just the failure you planned for, but also every bug
    // you didn't. The ones you didn't plan for get silently turned into
    // whatever fallback value this method returns, and you never learn they
    // happened. That's how "works on my machine" becomes "silently returns
    // wrong answers in production".
    //
    // Below is the anti-pattern. It looks safer than `ParseOrFallback`
    // (Lesson A) — "why name the type when I could just catch everything?"
    // — but watch what happens when we hand it `null`.

    // ✗ ANTI-PATTERN: swallows every exception type.
    public static int SwallowEverything_AntiPattern(string text)
    {
        // Looks innocent. For "abc" it returns 0 — same as ParseOrFallback.
        // But `text = null` is a DIFFERENT failure (ArgumentNullException,
        // not FormatException) — it signals a BUG in the caller, not
        // "invalid input". This catch silences both identically:
        //
        //   SwallowEverything_AntiPattern("abc")  == 0   ← fine, input was bad
        //   SwallowEverything_AntiPattern(null)   == 0   ← BUG. Silently hidden!
        //
        // Compare with `ParseOrFallback` from Lesson A: it only catches
        // FormatException. Pass null to that and ArgumentNullException
        // bubbles up — which is exactly what you want, because it reveals
        // the caller's bug instead of masking it.
        try
        {
            return int.Parse(text);
        }
        catch // ← no type filter == catch everything. Almost always wrong.
        {
            return 0;
        }
    }

    // ─────────────────────────────────────────────────────────────
    // LESSON E: When there's a non-throwing alternative, prefer it
    // ─────────────────────────────────────────────────────────────
    // Exceptions are for UNEXPECTED situations. If "the input might be
    // invalid" is a normal, everyday path — parsing user input, reading a
    // CSV field, looking up a key that might not exist — then it isn't
    // exceptional, and wrapping it in try/catch is the wrong tool.
    //
    // The .NET standard library has a naming convention for exactly this:
    // wherever a method `Xxx` might fail on expected inputs, there's
    // usually a paired `TryXxx` method that returns a `bool` (did it work?) and
    // hands the value back via an `out` parameter. No throw, no catch.
    //
    // You already met one of these in the Strings lesson: `int.TryParse`.
    // The same pattern runs right across the library:
    //
    //   int.Parse       / int.TryParse
    //   double.Parse    / double.TryParse
    //   DateTime.Parse  / DateTime.TryParse
    //   Enum.Parse<T>   / Enum.TryParse<T>              (you saw this in the Enums lesson)
    //   dict[key]       / dict.TryGetValue(key, out v)
    //
    // How to discover them: in your editor, type the type name and a dot
    // (e.g. `int.`) — IntelliSense shows the full method list, and `Parse`
    // always sits right next to `TryParse`. If you ever find yourself
    // writing `try { int.Parse(...) }` to handle expected-bad input,
    // reach for TryParse instead.
    //
    // The pattern isn't magic — it's just a convention. You can author your
    // own TryXxx method whenever you have a `Xxx` that throws on a
    // predictable bad input. Here's a TryXxx twin of `Divide` from Lesson B:
    // same job, but instead of throwing on b == 0 it returns `false` and
    // leaves `result` at 0. The caller decides what to do about it.
    public static bool TryDivide(int a, int b, out int result)
    {
        // e.g. TryDivide(10, 2, out r)  == true,  r == 5
        //      TryDivide(10, 0, out r)  == false, r == 0 (no throw)
        if (b == 0)
        {
            result = 0;
            return false;
        }
        result = a / b;
        return true;
    }

    public static int ParsePreferred(string text, int fallback)
    {
        // e.g. ParsePreferred("42", -1)  == 42
        //      ParsePreferred("abc", -1) == -1  ← no exception thrown at all
        //
        // Notice the shape: the bool return tells you if it worked; the
        // parsed value comes out through the `out` parameter. This is the
        // same `Try...(..., out ...)` pattern you'll see everywhere.
        if (int.TryParse(text, out int value))
        {
            return value;
        }
        return fallback;
    }
}
