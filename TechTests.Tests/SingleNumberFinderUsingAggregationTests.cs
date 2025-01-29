using AutoFixture;
using FluentAssertions;
using TechTests.Contracts;
using TechTests.Implementations;
using Xunit;

namespace TechTests.Tests
{
    public class SingleNumberFinderUsingAggregationTests
    {
        private readonly ISingleNumberFinder _sut;
        private readonly Fixture _fixture = new();

        public SingleNumberFinderUsingAggregationTests()
        {
            _sut = new SingleNumberFinderUsingAggregation();
        }

        [Fact]
        public void FindSingleNumber_ShouldReturnCorrectElement_WhenArrayContainsOneElement()
        {
            var expected = _fixture.Create<int>();
            var array = new[] { expected };

            var result = _sut.FindSingleNumber(array);

            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(new[] { 2, 2 })]
        [InlineData(new[] { 2, 2, 1, 1 })]
        [InlineData(new[] { 2, 3, 1, 2, 3, 1 })]
        public void FindSingleNumber_ShouldThrowInvalidOperationException_WhenElementsAppearsTwice(int[] nums)
        {
            var act = () => _sut.FindSingleNumber(nums);

            act.Should().Throw<InvalidOperationException>().WithMessage("All elements in the array are duplicates.");
        }

        [Theory]
        [InlineData(new[] { 2, 2, 1 }, 1)]
        [InlineData(new[] { 4, 1, 2, 1, 2 }, 4)]
        [InlineData(new[] { 1 }, 1)]
        public void FindSingleNumber_ShouldReturnCorrectResult_WhemElementsAppearsTwiceExceptForOne(int[] nums, int expected)
        {
            var result = _sut.FindSingleNumber(nums);

            result.Should().Be(expected);
        }
    }
}