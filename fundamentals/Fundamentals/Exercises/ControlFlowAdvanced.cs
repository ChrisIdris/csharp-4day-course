namespace Fundamentals.Exercises;

// Theme: Control flow (advanced) — exercises for you to implement.
// The teaching material is in Lessons/ControlFlowAdvanced.cs — read that first.
public static class ControlFlowAdvanced
{
    // EXERCISE 1: GradeFromMark
    // Return "A" for 70+, "B" for 60..69, "C" for 50..59, "F" for below 50.
    // Write this as a SWITCH EXPRESSION with relational patterns.
    // Example: GradeFromMark(85) → "A"; GradeFromMark(63) → "B"; GradeFromMark(40) → "F"
    // Hint: see Lesson J. Arms are tested top-to-bottom — put `>= 70` first.
    public static string GradeFromMark(int mark)
    {
        switch(mark)
        {
            case >= 70:
                return "A";
            case >= 60:
                return "B";
            case >= 50:
                return "C";
            default:
                return "F";
        }
    }

    // EXERCISE 2: TrafficLightAction
    // Return the action for a traffic-light colour:
    //   "red"                → "stop"
    //   "amber" or "yellow"  → "prepare"  (two colours sharing one arm)
    //   "green"              → "go"
    //   anything else        → "?"
    // Write this as a SWITCH EXPRESSION. Case-sensitive input is fine.
    // Example: TrafficLightAction("red") → "stop"; TrafficLightAction("yellow") → "prepare"
    // Hint: patterns can be literal strings. For two literals sharing one
    //       arm, separate them with `or`:
    //           "amber" or "yellow" => "prepare",
    public static string TrafficLightAction(string colour)
    {
        switch(colour)
        {
            case "red":
                return "stop";
            case "amber":
            case "yellow":
                return "prepare";
            case "green":
                return "go";
            default:
                return "?";
        }
    }
}
