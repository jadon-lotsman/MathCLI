using MathCLI.MathSyntax.ExpressionReader.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.Extensions
{
    public static class TokenTypeExtensions
    {
        public static TokenType GetGroup(this TokenType type)
            => (TokenType)((int)type & (int)TokenType.BitMask);

        public static bool IsOperator(this TokenType type)
            => type.GetGroup() == TokenType.Operator;
        public static bool IsFunction(this TokenType type)
            => type.GetGroup() == TokenType.Function;
        public static bool IsPunctuation(this TokenType type)
            => type.GetGroup() == TokenType.Punctuation;
    }
}
