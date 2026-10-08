using MathCLI.Extensions;
using MathCLI.MathSyntax;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MathCLI.MathSyntax.ExpressionReader
{
    public class ExpressionReader
    {
        public ReadResult Read(string source)
        {
            List<Token> tokens = new List<Token>();

            for (int i = 0; i < source.Length; i++)
            {
                char ch = source[i];

                if (ch.IsWhiteSpace())
                    continue;

                Token token;

                if (ch.IsDigit())
                {
                    token = CaptureNumber(source, ref i);
                }
                else if (ch.isLetter())
                {
                    token = CaptureFunctionOrVariable(source, ref i);
                }
                else if (ch.IsComma())
                {
                    token = Token.Comma(i);
                }
                else if (ch.IsLeftBracket())
                {
                    token = Token.OpenBracket(i);
                }
                else if (ch.IsRightBracket())
                {
                    token = Token.CloseBracket(i);
                }
                else if (Op.IsOperatorOrFunction(ch))
                {
                    token = Token.Operator(ch, i);
                }
                else
                {
                    token = Token.Unknown(i, 1);
                }

                tokens.Add(token);
            }

            return new ReadResult(source, tokens);
        }

        private Token CaptureNumber(string expr, ref int i)
        {
            var sb = new StringBuilder();
            sb.Append(expr[i]);

            bool hasSeparator = false;

            while (i + 1 < expr.Length)
            {
                char next = expr[i + 1];

                if (next.IsDigit())
                {
                    sb.Append(next);
                    i++;
                }
                else if ((next.IsPoint() || next.IsSlash()) && !hasSeparator)
                {
                    if (i + 2 < expr.Length && expr[i + 2].IsDigit())
                    {
                        sb.Append(next);
                        i++;
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

            string number = sb.ToString();

            if (!IsValidFraction(number))
                return Token.Unknown(i, number.Length);

            return Token.Number(number, i);
        }

        private bool IsValidFraction(string str)
        {
            return Regex.IsMatch(str, @"^[0-9]+(?:[./][0-9]+)?$");
        }

        private Token CaptureFunctionOrVariable(string expr, ref int i)
        {
            string str = expr[i].ToString();

            while (i + 1 < expr.Length && char.IsLetter(expr[i + 1]))
            {
                str += expr[++i];
            }

            if (Op.IsFunction(str))
                return Token.Function(str, i);

            if (str.Length <= 2)
                return Token.Variable(str, i);

            return Token.Unknown(i, str.Length);
        }
    }
}
