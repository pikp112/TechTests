using TechTests.Contracts;

namespace TechTests.Implementations
{
    public class SingleNumberFinderWithLoop : ISingleNumberFinder
    {
        public int FindSingleNumber(int[] nums)
        {
            if (nums.Length == 1)
                return nums[0];

            foreach (var num in nums)
            {
                int count = 0;

                foreach (var num2 in nums)
                {
                    if (num.Equals(num2))
                        count++;

                    if (count > 2)
                        throw new InvalidOperationException($"Number {num} appears more than twice in the array.");
                }

                if (count == 1)
                    return num;
            }

            throw new InvalidOperationException("No element appears exactly once.");
        }
    }
}