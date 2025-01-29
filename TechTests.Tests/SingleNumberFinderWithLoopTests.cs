using AutoFixture;
using FluentAssertions;
using TechTests.Contracts;
using TechTests.Implementations;
using Xunit;

namespace TechTests.Tests
{
    public class SingleNumberFinderWithLoopTests
    {
        private readonly ISingleNumberFinder _sut;
        private readonly Fixture _fixture = new();

        public SingleNumberFinderWithLoopTests()
        {
            _sut = new SingleNumberFinderWithLoop();
        }

        [Fact]
        public void FindSingleNumber_ShouldReturnSingleElement_WhenArrayContainsOneElement()
        {
            int expected = _fixture.Create<int>();
            var array = new[] { expected };

            var result = _sut.FindSingleNumber(array);

            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(new[] { 2, 2 })]
        [InlineData(new[] { 2, 2, 1, 1 })]
        [InlineData(new[] { 2, 3, 1, 2, 3, 1 })]
        public void FindSingleNumber_ShouldThrowInvalidOperationException_WhenNoElementAppearsExactlyOnce(int[] nums)
        {
            var act = () => _sut.FindSingleNumber(nums);

            act.Should().Throw<InvalidOperationException>().WithMessage("No element appears exactly once.");
        }

        [Theory]
        [InlineData(new[] { 2, 2, 2 }, 2)]
        [InlineData(new[] { 4, 1, 2, 1, 2, 4, 2, 2 }, 2)]
        [InlineData(new[] { 4, 1, 2, 1, 2, 3, 3, 4, 4 }, 4)]
        public void FindSingleNumber_ShouldThrowInvalidOperationException_WhenElementAppearsMoreThanTwoTimes(int[] nums, int expected)
        {
            var act = () => _sut.FindSingleNumber(nums);

            act.Should().Throw<InvalidOperationException>()
               .WithMessage($"Number {expected} appears more than twice in the array.");
        }

        [Theory]
        [InlineData(new[] { 2, 2, 1 }, 1)]
        [InlineData(new[] { 4, 1, 2, 1, 2 }, 4)]
        [InlineData(new[] { 4, 1, 2, 1, 2, 3, 3 }, 4)]
        public void FindSingleNumber_ShouldReturnCorrectResult_WhenOneElementAppearsExactlyOnce(int[] nums, int expected)
        {
            var result = _sut.FindSingleNumber(nums);

            result.Should().Be(expected);
        }
    }
}