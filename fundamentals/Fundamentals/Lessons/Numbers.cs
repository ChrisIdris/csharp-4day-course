namespace Fundamentals.Lessons;

// Theme: Numbers — integers, doubles, and casting between them.
// This file contains the TEACHING EXAMPLES only. Your work lives in
// Exercises/Numbers.cs.
public static class Numbers
{
    // ─────────────────────────────────────────────────────────────
    // LESSON A: What is an integer?
    // ─────────────────────────────────────────────────────────────
    // An `int` is a whole number (no fractional part) — e.g. -3, 0, 42.
    // In C#, `int` is 32-bit: it can hold values from about -2.1 billion
    // to +2.1 billion. Good enough for most counting/indexing work.

    // A method can take an int as a parameter (an "argument") and
    // return an int as a result.
    public static int Double(int x)
    {
        return x * 2;
    }

    // ─────────────────────────────────────────────────────────────
    // LESSON B: Declaring, using, and updating a variable
    // ─────────────────────────────────────────────────────────────
    // A variable is a named box that holds a value.
    // You declare it with a type and a name, then assign with `=`.
    public static int CountUpToFive()
    {
        int counter = 0;       // declare + assign initial value
        counter = counter + 1; // update: read current, add 1, write back
        counter = counter + 1; // (this is so common C# has a shortcut: counter++)
        counter++;             // same as counter = counter + 1
        counter++;
        counter++;
        return counter;        // we return the final value so the caller can use it
    }

    // ─────────────────────────────────────────────────────────────
    // LESSON C: Floating-point numbers (double)
    // ─────────────────────────────────────────────────────────────
    // A `double` is a number that CAN have a fractional part — e.g. 3.14, -0.5.
    // "Double" is short for "double-precision floating-point".
    // Any literal with a decimal point, like 3.14, is a double by default.
    public static double CircleArea(double radius)
    {
        double pi = 3.14159;
        return pi * radius * radius;
    }

    // ─────────────────────────────────────────────────────────────
    // LESSON D: Integer division truncates
    // ─────────────────────────────────────────────────────────────
    // If BOTH sides of `/` are integers, C# does integer division:
    // the fractional part is thrown away (truncated).
    // 7 / 2 is mathematically 3.5, but this returns 3.
    public static int DivideIntegers(int a, int b)
    {
        return a / b; // 7 / 2 == 3, not 3.5!
    }

    // ─────────────────────────────────────────────────────────────
    // LESSON E: Casting between types
    // ─────────────────────────────────────────────────────────────
    // "Casting" means converting a value from one type to another.
    //
    // Going from int → double is SAFE (no information lost),
    // so C# does it automatically (implicit conversion):
    public static double IntToDouble(int x)
    {
        double result = x; // no cast needed
        return result;
    }

    // Going from double → int LOSES the fractional part,
    // so C# forces you to be explicit about it with `(int)`:
    public static int DoubleToInt(double x)
    {
        return (int)x; // (int)3.9 gives 3 — it truncates toward zero
    }

    // Now we can fix Lesson D's truncation problem.
    // By casting ONE side to double first, the division becomes
    // a floating-point division and the fractional part is kept.
    public static double DivideWithCasting(int a, int b)
    {
        return (double)a / b; // 7 / 2 == 3.5 now
    }
}
