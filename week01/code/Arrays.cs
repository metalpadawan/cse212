public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.
        // Create a new double array with a length equal to the number of multiples requested.
        // Use a loop to go through each index in the array.
        // For each index, multiply the starting number by index + 1.
        // Store that value in the array.
        // After the loop finishes, return the completed array.

        double[] result = new double[length];

        for (int i = 0; i < length; i++)
        {
            result[i] = number * (i + 1);
        }

        return result; // replace this return statement with your own
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.
        // Find the index where the rotated list should start. This is data.Count - amount.
        // Make a temporary list that contains the last amount values from the original list.
        // Make another temporary list that contains the values before the starting index.
        // Clear the original list so it can be rebuilt in the rotated order.
        // Add the last values first because a right rotation moves them to the front.
        // Add the first values after that because they move to the back.
        // Your code goes here.
        // Start by creating startIndex with data.Count - amount.
        // Then use GetRange to create the two temporary lists.
        // Finally, clear data and add the two temporary lists back in the rotated order.
        int startIndex = data.Count - amount;
        
        List<int> endValues = data.GetRange(startIndex, amount);
        List<int> beginningValues = data.GetRange(0, startIndex);

        data.Clear();

        data.AddRange(endValues);
        data.AddRange(beginningValues);

    }
}
