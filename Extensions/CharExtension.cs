using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MathCLI.Extensions
{
    public static class CharExtension
    {
        public static bool IsDigit(this char ch) => char.IsDigit(ch);

        public static bool isLetter(this char ch) => char.IsLetter(ch);

        public static bool IsOperator(this char ch)
        {
            return Regex.IsMatch(ch.ToString(), @"[+\-\*\:\=]");
        }

        public static bool IsComma(this char ch)
        {
            return ch == ',' || ch == '.';
        }

        public static bool IsLeftBracket(this char ch)
        {
            return ch == '(';
        }

        public static bool IsRightBracket(this char ch)
        {
            return ch == ')';
        }
    }
}
