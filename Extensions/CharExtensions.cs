using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MathCLI.Extensions
{
    public static class CharExtensions
    {
        public static bool IsWhiteSpace(this char ch) => char.IsWhiteSpace(ch);

        public static bool IsDigit(this char ch) => char.IsDigit(ch);
        public static bool isLetter(this char ch) => char.IsLetter(ch);

        public static bool IsPoint(this char ch) => ch == '.';
        public static bool IsComma(this char ch) => ch == ',';
        public static bool IsLeftBracket(this char ch) =>  ch == '(';
        public static bool IsRightBracket(this char ch) => ch == ')';
    }
}
