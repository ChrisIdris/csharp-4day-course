namespace Fundamentals.Exercises;

// Theme: Lists — exercises for you to implement.
// The teaching material for this theme is in Lessons/Lists.cs — read that first.
public static class Lists
{
    // EXERCISE 1: SumOfList
    // Return the sum of all elements in the list.
    // Example: SumOfList([1, 2, 3, 4]) → 10
    // Example: SumOfList([]) → 0
    // Hint: Lesson F showed two ways to iterate a list — pick one and accumulate a total.
    public static int SumOfList(List<int> numbers)
    {
        throw new NotImplementedException("TODO: iterate and accumulate a total");
    }

    // EXERCISE 2: FindLargest
    // Return the largest value in the list. You may assume the list has at
    // least one element.
    // Example: FindLargest([3, 1, 4, 1, 5, 9, 2, 6]) → 9
    // Hint: start by assuming the first element is the largest, then iterate
    //       and update your "largest so far" whenever you see something bigger.
    public static int FindLargest(List<int> numbers)
    {
        throw new NotImplementedException("TODO: track the largest seen so far");
    }

    // EXERCISE 3: CountEvens
    // Return the count of even numbers in the list.
    // Example: CountEvens([1, 2, 3, 4, 5, 6]) → 3
    // Example: CountEvens([1, 3, 5]) → 0
    // Hint: a number is even when `n % 2 == 0`. Increment a counter each time.
    public static int CountEvens(List<int> numbers)
    {
        throw new NotImplementedException("TODO: count values where n % 2 == 0");
    }

    // EXERCISE 4: CountPositives
    // Return how many elements of `numbers` are strictly positive (> 0).
    // Example: CountPositives([3, -1, 0, 7, -5]) → 2
    // Hint: foreach + if. See Lesson F.
    public static int CountPositives(List<int> numbers)
    {
        throw new NotImplementedException("TODO: foreach over numbers, increment a counter when > 0");
    }

    // EXERCISE 5: FirstIndexOf
    // Return the INDEX of the first occurrence of `target` in `numbers`, or -1 if not found.
    // Write the loop yourself — don't just call list.IndexOf.
    // Example: FirstIndexOf([10, 20, 30, 20], 20) → 1
    // Example: FirstIndexOf([1, 2, 3], 9) → -1
    // Hint: classic `for` loop — you need the index here, not just the value.
    //       Return `i` as soon as you find a match; return -1 after the loop.
    public static int FirstIndexOf(List<int> numbers, int target)
    {
        throw new NotImplementedException("TODO: for loop with index; return i when numbers[i] == target; else -1");
    }

    // EXERCISE 6: SumUntilNegative
    // Sum the elements of `numbers`, but STOP as soon as you hit a negative number.
    // The negative number itself is NOT included in the sum.
    // Example: SumUntilNegative([1, 2, 3, -1, 10]) → 6
    // Example: SumUntilNegative([1, 2, 3]) → 6
    // Hint: foreach + if + break.
    public static int SumUntilNegative(List<int> numbers)
    {
        throw new NotImplementedException("TODO: foreach; if n < 0 break; otherwise add to total");
    }

    // EXERCISE 7: ReverseList
    // Return a NEW List<int> with the elements of `numbers` in reverse order.
    // Do NOT modify the input list. Do NOT use list.Reverse().
    // Example: ReverseList([1, 2, 3, 4]) → [4, 3, 2, 1]
    // Example: ReverseList([]) → []
    // Hint: start with `new List<int>()`, then use a for loop that walks the
    //       input from the last index down to 0, calling Add on the new list.
    public static List<int> ReverseList(List<int> numbers)
    {
        throw new NotImplementedException("TODO: new List<int>(); walk input last-to-first; Add each element");
    }
}
