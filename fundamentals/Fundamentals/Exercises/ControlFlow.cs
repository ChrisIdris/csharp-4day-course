namespace Fundamentals.Exercises;

// Theme: Control flow — exercises for you to implement.
// The teaching material for this theme is in Lessons/ControlFlow.cs — read that first.
public static class ControlFlow
{
    // EXERCISE 1: Sign
    // Return "negative", "zero", or "positive" for the given integer.
    // Example: Sign(-3) → "negative"; Sign(0) → "zero"; Sign(7) → "positive"
    // Hint: if / else if / else chain. See Lesson A.
    public static string Sign(int n)
    {
        if(n == 0)
        {
            return "zero";
        }
        else if(n < 0)
        {
            return "negative";
        }
        else
        {
            return "positive";
        }
        throw new NotImplementedException("TODO: compare n against 0 with if / else if / else");
    }

    // EXERCISE 2: IsLeapYear
    // Return true if `year` is a leap year under the Gregorian rules:
    //   - divisible by 4, EXCEPT
    //   - years divisible by 100 are NOT leap years, EXCEPT
    //   - years divisible by 400 ARE leap years
    // Example: IsLeapYear(2024) → true; IsLeapYear(1900) → false; IsLeapYear(2000) → true
    // Hint: combine conditions with && and ||, or nest ifs. Either approach works.
    public static bool IsLeapYear(int year)
    {
        if(year % 4 == 0)
        {
            if(year % 100 == 0)
            {
                if(year % 400 == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }
        //throw new NotImplementedException("TODO: apply the divisible-by-4 / 100 / 400 rules");
    }

    // EXERCISE 3: DayName
    // Return the 3-letter name of the day for values 1..7, or "?" for anything else.
    // Example: DayName(1) → "Mon"; DayName(7) → "Sun"; DayName(42) → "?"
    // Hint: switch STATEMENT with 7 cases + default. See Lesson D.
    public static string DayName(int day)
    {
        string dayName = "";
        switch (day)
        {
            case 1:
                dayName = "Mon";
                break;
            case 2:
                dayName = "Tue";
                break;
            case 3:
                dayName = "Wed";
                break;
            case 4:
                dayName = "Thu";
                break;
            case 5:
                dayName = "Fri";
                break;
            case 6:
                dayName = "Sat";
                break;
            case 7:
                dayName = "Sun";
                break;

        }
        return dayName;
        //throw new NotImplementedException("TODO: switch on day, return the 3-letter name");
    }

    // EXERCISE 4: IsVowel
    // Return true if `c` is a vowel (a, e, i, o, u), case-insensitive.
    // Example: IsVowel('a') → true; IsVowel('E') → true; IsVowel('z') → false
    // Hint: switch STATEMENT with stacked case labels. Lowercase first via char.ToLower(c).
    public static bool IsVowel(char c)
    {
        if (char.ToLower(c) == 'a' || char.ToLower(c) == 'e' || char.ToLower(c) == 'i' || char.ToLower(c) == 'o' || char.ToLower(c) == 'u')
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    // EXERCISE 5: CountPositives
    // Return how many elements of `nums` are strictly positive (> 0).
    // Example: CountPositives([3, -1, 0, 7, -5]) → 2
    // Hint: foreach + if. See Lesson H.
    public static int CountPositives(int[] nums)
    {
        int positives = 0;
        foreach (int num in nums)
        {
            if (num > 0)
            {
                positives++;
            }
        }
        return positives;
    }

    // EXERCISE 6: FirstIndexOf
    // Return the INDEX of the first occurrence of `target` in `nums`, or -1 if not found.
    // Example: FirstIndexOf([10, 20, 30, 20], 20) → 1; FirstIndexOf([1, 2, 3], 9) → -1
    // Hint: classic `for` loop — you need the index here, not just the value.
    //       Return `i` as soon as you find a match.
    public static int FirstIndexOf(int[] nums, int target)
    {
        for (int i = 0; i < nums.Length; i++ )
        {
            if (nums[i] == target) return i;

        }
        return -1;
    }

    // EXERCISE 7: SumUntilNegative
    // Sum the elements of `nums`, but STOP as soon as you hit a negative number.
    // The negative number itself is NOT included in the sum.
    // Example: SumUntilNegative([1, 2, 3, -1, 10]) → 6; SumUntilNegative([1, 2, 3]) → 6
    // Hint: foreach + if + break. See Lesson F for break.
    public static int SumUntilNegative(int[] nums)
    {
        throw new NotImplementedException("TODO: foreach; if n < 0 break; otherwise add to total");
    }

    // EXERCISE 8: ReverseArray
    // Return a NEW int[] with the elements of `nums` in reverse order.
    // Do NOT modify the input array.
    // Example: ReverseArray([1, 2, 3, 4]) → [4, 3, 2, 1]
    // Hint: allocate `new int[nums.Length]`, then use a for loop to copy
    //       nums[nums.Length - 1 - i] into result[i].
    public static int[] ReverseArray(int[] nums)
    {
        int[] newNums = new int[nums.Length];
        for(int i = 0; i < nums.Length; i++)
        {
            newNums[i] = nums[nums.Length - 1 - i];
        }
        return newNums;
        throw new NotImplementedException("TODO: allocate a new int[], copy nums in reverse order");
    }
}
