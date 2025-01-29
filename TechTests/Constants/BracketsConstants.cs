namespace TechTests.Constants
{
    public static class BracketsConstants
    {
        public const string VALID_BRACKETS = "(){}[]";

        public static readonly Dictionary<char, char> BRACKET_PAIRS = new()
        {
            { '(', ')' },
            { '{', '}' },
            { '[', ']' }
        };
    }
}