namespace Fundamentals.Exercises;

// Theme: Structs — exercises for you to implement.
// The teaching material for this theme is in Lessons/Structs.cs — read that first.
//
// You're building a Point struct that represents a 2D coordinate.
// Implement the constructor, then each method marked TODO.

public struct Point
{
    // Properties are already declared for you — no work needed here.
    public double X { get; set; }
    public double Y { get; set; }

    // EXERCISE 1: constructor
    // Assign `x` to X and `y` to Y so `new Point(3, 4)` produces a Point
    // whose X is 3 and Y is 4.
    // Hint: Lesson C in Structs.cs shows the pattern exactly.
    public Point(double x, double y)
    {
        X = x;
        Y = y;
    }

    // EXERCISE 2: IsOrigin
    // Return true when this Point is (0, 0), otherwise false.
    // Example: new Point(0, 0).IsOrigin() → true
    // Example: new Point(3, 4).IsOrigin() → false
    public bool IsOrigin()
    {
        if (X == 0 || Y == 0) return true;
        else return false;
    }

    // EXERCISE 3: DistanceTo
    // Return the straight-line (Euclidean) distance from THIS point to `other`.
    // Formula: sqrt( (this.X - other.X)^2 + (this.Y - other.Y)^2 )
    // Use Math.Sqrt and Math.Pow (or just multiply a value by itself).
    // Example: new Point(0, 0).DistanceTo(new Point(3, 4)) → 5
    public double DistanceTo(Point other)
    {
        var distance = Math.Sqrt(Math.Pow(this.X - other.X, 2) + Math.Pow(this.Y - other.Y, 2));
        return distance;
    }

    // EXERCISE 4: Translate
    // Return a NEW Point shifted by (dx, dy). Do NOT mutate this instance —
    // value-type idiom says: return a new value rather than mutate.
    // Example: new Point(1, 2).Translate(10, 20) → new Point(11, 22)
    public Point Translate(double dx, double dy)
    {
        return new Point(X + dx, Y + dy);
    }
}
