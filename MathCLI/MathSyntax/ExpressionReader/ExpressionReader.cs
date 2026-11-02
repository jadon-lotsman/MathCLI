using MathCLI.Extensions;
using MathCLI.MathKernel.Variables;
using MathCLI.MathSyntax;
using MathCLI.MathSyntax.ExpressionReader.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MathCLI.MathSyntax.ExpressionReader
{
    public class ExpressionReader
    {
        public ReadResult Read(ReadOnlySpan<char> span)
        {
            int pos = 0;
            var parsed = new List<Token>();

            while (pos < span.Length)
            {
                if (span[pos].IsWhiteSpace())
                {
                    pos++;
                    continue;
                }

                var token = GetToken(span, ref pos);
                parsed.Add(token);
            }

            return new ReadResult(span.ToString(), parsed);
        }

        private Token GetToken(ReadOnlySpan<char> span, ref int pos)
        {
            int start = pos;
            char ch = span[pos];

            if (char.IsDigit(ch))
                return CaptureNumber(span, ref pos);

            if (char.IsLetter(ch))
                return CaptureFunctionOrVariable(span, ref pos);

            TokenType type = ch switch
            {
                ',' => TokenType.Comma,
                '(' => TokenType.OpenBracket,
                ')' => TokenType.CloseBracket,
                '+' => TokenType.Sum,
                '-' => TokenType.Sub,
                '*' => TokenType.Mul,
                '/' => TokenType.Div,
                '^' => TokenType.Pow,
                _ => TokenType.Unknown,
            };

            pos = start + 1;
            return Token.Create(type, start, 1);
        }

        private Token CaptureNumber(ReadOnlySpan<char> span, ref int pos)
        {
            int start = pos;
            int i = pos + 1;
            bool usedSeparator = false;

            while (i < span.Length)
            {
                char c = span[i];

                if (c.IsDigit())
                {
                    i++;
                }
                else if (!usedSeparator && (c.IsPoint() || c.IsSlash())
                         && i + 1 < span.Length && span[i + 1].IsDigit())
                {
                    usedSeparator = true;
                    i++;
                }
                else
                {
                    break;
                }
            }

            pos = i;
            return Token.Create(TokenType.Number, start, i-start);
        }

        private Token CaptureFunctionOrVariable(ReadOnlySpan<char> span, ref int pos)
        {
            int start = pos;
            int i = pos;

            while (i < span.Length && char.IsLetter(span[i]))
                i++;

            int length = i - start;
            var name = span.Slice(start, length);

            pos = i;

            if (name.SequenceEqual("pow"))
                return Token.Create(TokenType.Powf, start, length);

            return Token.Create(TokenType.Variable, start, length);
        }
    }
}
