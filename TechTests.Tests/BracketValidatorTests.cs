using FluentAssertions;
using TechTests.Contracts;
using TechTests.Implementations;
using TechTests.Models;
using Xunit;

namespace TechTests.Tests
{
    public class BracketValidatorTests
    {
        private readonly IBracketValidator _sut;

        public BracketValidatorTests()
        {
            _sut = new BracketValidator();
        }

        [Fact]
        public void AreBracketsBalanced_ShouldThrowArgumentNullException_WhenExpressionIsNull()
        {
            string? expression = null;

            var act = () => _sut.AreBracketsBalanced(expression);

            act.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("\t")]
        public void AreBracketsBalanced_ShouldThrowInvalidOperationException_WhenExpressionIsEmptyOrWhiteSpace(string expression)
        {
            var act = () => _sut.AreBracketsBalanced(expression);

            act.Should().Throw<InvalidOperationException>()
               .WithMessage("Unable to check for balanced brackets because expression is null or empty.");
        }

        [Fact]
        public void AreBracketsBalanced_ShouldReturnNotBalanced_WhenExpressionLengthIsOne()
        {
            var expression = "[";

            var result = _sut.AreBracketsBalanced(expression);

            result.Should().Be(BracketBalanceStatus.NotBalanced);
        }

        [Fact]
        public void AreBracketsBalanced_ShouldThrowInvalidOperationException_WhenExpressionContainsInvalidCharacters()
        {
            var expression = "[a{b}]";

            var act = () => _sut.AreBracketsBalanced(expression);

            act.Should().Throw<InvalidOperationException>()
               .WithMessage($"Invalid character 'a' found in expression.");
        }

        [Theory]
        [InlineData("{}")]
        [InlineData("{()}")]
        [InlineData("{([])}")]
        [InlineData("{([])}([])")]
        [InlineData("{([])}([{}])")]
        [InlineData("[()]{}{[()()]()}")]
        public void AreBracketsBalanced_ShouldReturnBalanced_WhenExpressionContainsBalancedBrackets(string expression)
        {
            var result = _sut.AreBracketsBalanced(expression);

            result.Should().Be(BracketBalanceStatus.Balanced);
        }

        [Theory]
        [InlineData("{]")]
        [InlineData("[(])")]
        [InlineData("[({])}")]
        [InlineData("[({]()})")]
        public void AreBracketsBalanced_ShouldReturnNotBalanced_WhenExpressionContainsUnbalancedBrackets(string expression)
        {
            var result = _sut.AreBracketsBalanced(expression);

            result.Should().Be(BracketBalanceStatus.NotBalanced);
        }
    }
}