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

            var v = new ReadResult(span.ToString(), parsed);

            for (int i = 0; i < parsed.Count; i++)
            {
                var t = v.TextOf(i).ToString();
                Console.Write(t);
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
            bool hasSeparator = false;

            while (pos + 1 < span.Length)
            {
                char next = span[pos + 1];

                if (next.IsDigit())
                {
                    pos++;
                }
                else if ((next.IsPoint() || next.IsSlash()) && !hasSeparator)
                {
                    if (pos + 2 < span.Length && span[pos + 2].IsDigit())
                    {
                        pos++;
                        hasSeparator = true;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            pos++;
            int length = pos - start;
            return Token.Create(TokenType.Number, start, length);
        }

        private Token CaptureFunctionOrVariable(ReadOnlySpan<char> span, ref int pos)
        {
            int start = pos;
            while (pos < span.Length && char.IsLetter(span[pos]))
            {
                pos++;
            }

            int length = pos - start;
            var name = span.Slice(start, length);

            if (name.SequenceEqual("pow"))
                return Token.Create(TokenType.Powf, start, length);

            if (name.Length <= 2)
                return Token.Create(TokenType.Variable, start);

            return Token.Create(TokenType.Unknown, start, length);
        }
    }
}
