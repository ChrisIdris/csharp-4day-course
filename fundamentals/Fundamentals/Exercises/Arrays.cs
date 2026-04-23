namespace Fundamentals.Exercises;

// Theme: Arrays — exercises for you to implement.
// The teaching material for this theme is in Lessons/Arrays.cs — read that first.
public static class Arrays
{
    // EXERCISE 1: SumOfArray
    // Return the sum of all elements in the array.
    // Example: SumOfArray([1, 2, 3, 4]) → 10
    // Example: SumOfArray([]) → 0
    // Hint: Lesson F showed two ways to iterate — pick one and accumulate a total.
    public static int SumOfArray(int[] numbers)
    {
        int total = 0;
        foreach (int number in numbers)
        {
            total += number;
        }
        return total;
        //throw new NotImplementedException("TODO: iterate and accumulate a total");
    }

    // EXERCISE 2: FindLargest
    // Return the largest value in the array. You may assume the array has at
    // least one element.
    // Example: FindLargest([3, 1, 4, 1, 5, 9, 2, 6]) → 9
    // Hint: start by assuming the first element is the largest, then iterate
    //       from index 1 onwards and update your "largest so far" whenever you
    //       see something bigger.
    public static int FindLargest(int[] numbers)
    {
        int largestNumber = 0;
        foreach (int number in numbers)
        {
            if(number > largestNumber)
            {
                largestNumber = number;
            }
        }
        return largestNumber;
        //throw new NotImplementedException("TODO: track the largest seen so far");
    }

    // EXERCISE 3: CountEvens
    // Return the count of even numbers in the array.
    // Example: CountEvens([1, 2, 3, 4, 5, 6]) → 3
    // Example: CountEvens([1, 3, 5]) → 0
    // Hint: a number is even when `n % 2 == 0`. Increment a counter each time.
    public static int CountEvens(int[] numbers)
    {
        int count = 0;
        foreach (int number in numbers)
        {
            if (number % 2 == 0)
            {
                count++;
            }
        }
        return count;
        //throw new NotImplementedException("TODO: count values where n % 2 == 0");
    }

    // EXERCISE 4: ReverseArray
    // Return a NEW array with the elements of `numbers` in reverse order.
    // Must not modify the input. Must not use Array.Reverse — write it yourself.
    // Example: ReverseArray([1, 2, 3]) → [3, 2, 1]
    // Example: ReverseArray([]) → []
    // Hint: allocate a new int[numbers.Length], then use a for loop where the
    //       source index and the destination index move in opposite directions.
    public static int[] ReverseArray(int[] numbers)
    {
        int[] reversedArray = new int[numbers.Length];
        int j= 0;
        for (int i = numbers.Length - 1; i >= 0; i--)
        {
            reversedArray[j] = numbers[i];
            j++;
        }

        throw new NotImplementedException("TODO: build a new array with indices walking in opposite directions");
    }
}
