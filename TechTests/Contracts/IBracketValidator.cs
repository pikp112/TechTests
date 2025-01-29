using TechTests.Models;

namespace TechTests.Contracts
{
    public interface IBracketValidator
    {
        BracketBalanceStatus AreBracketsBalanced(string? expression);
    }
}