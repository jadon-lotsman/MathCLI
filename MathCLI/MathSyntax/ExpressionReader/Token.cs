using MathCLI.MathSyntax.ExpressionReader.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MathCLI.MathSyntax.ExpressionReader
{
    public readonly record struct Token(TokenType Kind, int Position, int Length, string? Value = null)
    {
        public static Token Unknown(int position, int length) =>
            new Token(TokenType.Unknown, position, length);

        public static Token Variable(string value, int position) =>
            new Token(TokenType.Variable, position, value.Length, value);

        public static Token Number(string value, int position) =>
            new Token(TokenType.Number, position, value.Length, value);

        public static Token Operator(char value, int position) =>
            new Token(TokenType.Operator, position, 1, value.ToString());

        public static Token Function(string value, int position) =>
            new Token(TokenType.Function, position, value.Length, value);

        public static Token OpenBracket(int position) =>
            new Token(TokenType.OpenBracket, position, 1);

        public static Token CloseBracket(int position) =>
            new Token(TokenType.CloseBracket, position, 1);

        public static Token Comma(int position) =>
            new Token(TokenType.Comma, position, 1);
    }
}
