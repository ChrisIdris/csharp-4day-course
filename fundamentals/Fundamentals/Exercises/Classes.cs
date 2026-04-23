namespace Fundamentals.Exercises;

// Theme: Classes — exercises for you to implement.
// The teaching material for this theme is in Lessons/Classes.cs — read that first.
//
// You're building a ShoppingCart class that holds a list of item names and
// lets callers add, remove, check, and count. Implement the constructor and
// each method marked TODO.
//
// Because classes are REFERENCE types, when your tests share a cart between
// operations, the same object is mutated across calls — exactly like in
// Lesson E of the Classes lesson.

public class ShoppingCart
{
    // The internal list of items. Already declared for you.
    public List<string> Items { get; set; }
    // EXERCISE 1: constructor
    // Initialise Items to an empty `new List<string>()` so the cart starts
    // empty and every method below has a real list to work with (not null).
    // Hint: Lesson C in Classes.cs shows the Course constructor doing
    //       exactly this pattern for its Students list.

    public ShoppingCart()
    {
        Items = new List<string>();
    }

    // EXERCISE 2: Add
    // Append `item` to Items.
    // Example: cart.Add("apple"); cart.Count() → 1
    public void Add(string item)
    {
        Items.Add(item);
    }

    // EXERCISE 3: Count
    // Return how many items are currently in the cart.
    // Example: empty cart → 0; after two Add calls → 2
    public int Count()
    {
        return Items.Count;
    }

    // EXERCISE 4: Contains
    // Return true if `item` is in the cart, false otherwise.
    // Example: cart with "apple" → Contains("apple") is true, Contains("pear") is false
    // Hint: List<T> has a built-in .Contains method — you can delegate to it.
    public bool Contains(string item)
    {
        return Items.Contains(item);
    }

    // EXERCISE 5: Remove
    // Remove the first occurrence of `item` from the cart. Return true if
    // something was removed, false if `item` wasn't there.
    // Example: cart = ["apple", "pear"]; Remove("apple") → true, cart now ["pear"]
    // Example: cart = ["apple"];          Remove("pear")  → false, cart unchanged
    // Hint: List<T>.Remove(item) already returns the bool you need.
    public bool Remove(string item)
    {
        return Items.Remove(item);
    }
}
