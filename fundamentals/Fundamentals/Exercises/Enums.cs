namespace Fundamentals.Exercises;

// Theme: Enums — exercises for you to implement.
// The teaching material for this theme is in Lessons/Enums.cs — read that first.
//
// You're building a tiny vending-machine controller. The states are given;
// you implement the methods that reason about them.

public enum VendingMachineState
{
    Idle,
    CoinInserted,
    Dispensing,
    OutOfStock,
}

public static class Enums
{
    // EXERCISE 1: Prompt
    // Return the message the machine should display for each state:
    //   Idle          → "Insert a coin"
    //   CoinInserted  → "Select a product"
    //   Dispensing    → "Please wait..."
    //   OutOfStock    → "Sold out"
    // Use a `switch` statement — each case should `return` its string.
    // Include a `default` branch that throws ArgumentOutOfRangeException.
    // Hint: Lesson C in Enums.cs shows the exact shape.
    public static string Prompt(VendingMachineState state)
    {
        switch(state)
        {
            case VendingMachineState.Idle:
                return "Insert a coin";
            case VendingMachineState.CoinInserted:
                return "Select a product";
            case VendingMachineState.Dispensing:
                return "Please wait...";
            case VendingMachineState.OutOfStock:
                return "Sold out";
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    // EXERCISE 2: CanAcceptCoin
    // Return true only when the machine is in a state where a coin would be
    // accepted. A coin is only accepted when the machine is Idle.
    // Example: CanAcceptCoin(VendingMachineState.Idle)         → true
    //          CanAcceptCoin(VendingMachineState.CoinInserted) → false
    //          CanAcceptCoin(VendingMachineState.OutOfStock)   → false
    // Hint: enum values compare with `==` — no switch needed for this one.
    public static bool CanAcceptCoin(VendingMachineState state)
    {
        if(state == VendingMachineState.Idle)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // EXERCISE 3: ParseState
    // Parse a (possibly user-typed) string into a VendingMachineState.
    // Return true on success and set `state` to the matching value;
    // return false on failure (leave `state` at its default).
    // Matching should be CASE-INSENSITIVE: "idle", "Idle" and "IDLE" all work.
    // Example: ParseState("Dispensing", out var s) → true,  s == VendingMachineState.Dispensing
    //          ParseState("idle", out var s)       → true,  s == VendingMachineState.Idle
    //          ParseState("dancing", out var s)    → false
    // Hint: Enum.TryParse<T>(text, ignoreCase: true, out state) does exactly this.
    public static bool ParseState(string text, out VendingMachineState state)
    {
    }
}
