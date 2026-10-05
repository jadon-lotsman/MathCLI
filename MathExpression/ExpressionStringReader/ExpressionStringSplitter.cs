using MathCLI.Extensions;
using MathCLI.MathExpression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MathCLI.Term.ExpressionStringReader
{
    public static class ExpressionStringSplitter
    {
        public static string[] SplitAsExpression(string str)
        {
            List<string> splitted = new List<string>();
            str = str.RemoveMultispaces();

            for (int i = 0; i < str.Length; i++)
            {
                char ch = str[i];
                string token = null;

                if (ch.IsDigit())
                {
                    token = CaptureNumber(str, ref i);
                }
                else if (ch.isLetter())
                {
                    token = CaptureWord(str, ref i);
                }
                else if (ch.IsComma())
                {
                    token = ",";
                }
                else if (ch.IsLeftBracket())
                {
                    token = "(";
                }
                else if (ch.IsRightBracket())
                {
                    token = ")";
                }
                else if (ch.IsOperator())
                {
                    token = ch.ToString();
                }
                else
                {
                    throw new Exception("Unknown token");
                }

                splitted.Add(token);
            }

            return splitted.ToArray();
        }

        static private string PrepareNumber(string value)
        {
            if (Regex.IsMatch(value, @"^[0-9]+[.][0-9]+$"))
            {
                value = value.Replace('.', ',');
                double d = Convert.ToDouble(value);

                Fraction f = new Fraction(d);

                return $"{f.Numerator}/{f.Denominator}";
            }
            else if (Regex.IsMatch(value, @"^[0-9]+[/][0-9]+$"))
            {
                return value;
            }
            else if (Regex.IsMatch(value, @"^[0-9]+$"))
            {
                return $"{value}/1";
            }
            else
            {
                throw new Exception("Unknown number format");
            }
        }

        private static string CaptureNumber(string expr, ref int i)
        {
            string number = "" + expr[i];

            while (i + 1 < expr.Length && (char.IsDigit(expr[i + 1]) || expr[i + 1] == '.' || expr[i + 1] == '/'))
            {
                number += expr[++i];
            }

            number = PrepareNumber(number);

            return number;
        }

        static private string CaptureWord(string expr, ref int i)
        {
            string letter = "" + expr[i];

            while (i + 1 < expr.Length && char.IsLetter(expr[i + 1]))
            {
                letter += expr[++i];
            }

            return letter;
        }
    }
}
