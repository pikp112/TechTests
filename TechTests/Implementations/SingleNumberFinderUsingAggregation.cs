using TechTests.Contracts;

namespace TechTests.Implementations
{
    public class SingleNumberFinderUsingAggregation : ISingleNumberFinder
    {
        public int FindSingleNumber(int[] nums)
        {
            if (nums.Length == 1)
                return nums[0];

            var result = nums.Aggregate((num1, num2) => num1 ^ num2);

            if (result.Equals(0))
                throw new InvalidOperationException("All elements in the array are duplicates.");

            return result;
        }
    }
}