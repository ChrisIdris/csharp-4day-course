namespace Fundamentals.Exercises;

// Theme: Arrays (advanced) — exercises for you to implement.
// The teaching material is in Lessons/ArraysAdvanced.cs.
// Tackle this only after finishing the core Arrays exercises.
public static class ArraysAdvanced
{
    // ADVANCED EXERCISE 1: Transpose
    //
    // Given a rectangular matrix `int[,]` of shape [rows, cols], return a NEW
    // `int[,]` of shape [cols, rows] where output[c, r] == input[r, c].
    //
    // Example:
    //   input  (2 rows × 3 cols):   { {1, 2, 3},
    //                                 {4, 5, 6} }
    //   output (3 rows × 2 cols):   { {1, 4},
    //                                 {2, 5},
    //                                 {3, 6} }
    //
    // Hint:
    //   • rows  = matrix.GetLength(0)
    //   • cols  = matrix.GetLength(1)
    //   • allocate a new int[cols, rows]
    //   • nested for loops: for r in rows, for c in cols, assign
    //     result[c, r] = matrix[r, c].
    public static int[,] Transpose(int[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        int[,] result = new int[cols, rows];

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                result[c, r] = matrix[r, c];
            }
        }

        return result;
       // throw new NotImplementedException("TODO: new int[cols, rows], copy with indices swapped");
    }

    // ADVANCED EXERCISE 2: HasDuplicates
    //
    // Return true if any value appears more than once in the array, false
    // otherwise.
    //
    // Example: HasDuplicates([1, 2, 3]) → false
    // Example: HasDuplicates([1, 2, 1]) → true
    // Example: HasDuplicates([]) → false
    //
    // Hint: nested loops. For each index i, check every index j > i to see
    //       whether numbers[i] == numbers[j]. If you find a match, return true.
    //       Only return false once every pair has been checked.
    public static bool HasDuplicates(int[] numbers)
    {
        for (int i = 0; i < numbers.Length; i++)
        {
            for (int j = i + 1; j < numbers.Length; j++)
            {
                if (numbers[i] == numbers[j])
                {
                    return true;
                }
            }
        }
        return false;
    }
}
