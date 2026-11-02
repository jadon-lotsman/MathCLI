using MathCLI.Extensions;
using MathCLI.MathSyntax.ExpressionReader.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MathCLI.MathSyntax.ExpressionReader
{
    public readonly record struct Token(TokenType Type, int Start, int Length)
    {
        public static Token Create(TokenType type, int start, int length = 1) =>
            new Token(type, start, length);


        public bool IsMatch(TokenType type) => Type == type;
        public bool IsAnyMatch(params TokenType[] types)
        {
            foreach (var t in types)
                if (Type == t) return true;
            return false;
        }

        public bool IsOperator()    => Type.IsOperator();
        public bool IsFunction()    => Type.IsFunction();
        public bool IsPunctuation() => Type.IsPunctuation();
    }
}
