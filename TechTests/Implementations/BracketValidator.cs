using TechTests.Constants;
using TechTests.Contracts;
using TechTests.Models;

namespace TechTests.Implementations
{
    public class BracketValidator : IBracketValidator
    {
        public BracketBalanceStatus AreBracketsBalanced(string? expression)
        {
            ArgumentNullException.ThrowIfNull(expression, nameof(expression));

            if (string.IsNullOrWhiteSpace(expression))
                throw new InvalidOperationException("Unable to check for balanced brackets because expression is null or empty.");

            if (expression.Length == 1)
                return BracketBalanceStatus.NotBalanced;

            Stack<char> stackExpression = new();

            foreach (var bracket in expression)
            {
                if (!BracketsConstants.VALID_BRACKETS.Contains(bracket))
                    throw new InvalidOperationException($"Invalid character '{bracket}' found in expression.");

                if (stackExpression.Count == 0)
                    stackExpression.Push(bracket);
                else if (IsBracketPair(stackExpression.Peek(), bracket))
                    stackExpression.Pop();
                else
                    stackExpression.Push(bracket);
            }

            return stackExpression.Count == 0
                ? BracketBalanceStatus.Balanced
                : BracketBalanceStatus.NotBalanced;
        }

        private bool IsBracketPair(char openingBracket, char closingBracket)
        {
            //return (openingBracket == '(' && closingBracket == ')')
            //    || (openingBracket == '{' && closingBracket == '}')
            //    || (openingBracket == '[' && closingBracket == ']');
            return BracketsConstants.BRACKET_PAIRS.TryGetValue(openingBracket, out char expectedClosingBracket) && expectedClosingBracket == closingBracket;
        }
    }
}