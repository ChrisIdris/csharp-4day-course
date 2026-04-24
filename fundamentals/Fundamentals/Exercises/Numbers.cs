namespace Fundamentals.Exercises;

// Theme: Numbers — exercises for you to implement.
// The teaching material for this theme is in Lessons/Numbers.cs — read that first.
public static class Numbers
{
    // EXERCISE 1:
    // Return the sum of two integers.
    public static int Add(int a, int b)
    {
        return a + b;
        throw new NotImplementedException("TODO: return a + b");
    }

    // EXERCISE 2:
    // Return the area of a rectangle (width × height).
    public static double RectangleArea(double width, double height)
    {
        return width * height;
    }

    // EXERCISE 3:
    // Convert temperature from Celsius to Fahrenheit.
    // Formula: F = C × 9 / 5 + 32.
    // Tip: remember Lesson D — if you use 9 / 5 with integers, you'll get 1,
    // not 1.8. Cast appropriately so you get the right answer.
    public static double CelsiusToFahrenheit(double celsius)
    {
        return celsius * 9 / 5 + 32;
    }
}
