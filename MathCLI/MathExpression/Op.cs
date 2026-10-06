using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathExpression
{
    public static class Op
    {
        public const string Plus = "+";
        public const string Minus = "-";
        public const string Multiply = "*";
        public const string Divide = "/";
        public const string Power = "^";
        public const string Pow = "pow";
        //public const string Sin = "sin";
        //public const string Cos = "cos";
        //public const string Tg = "tg";
        //public const string Tan = "tan";
        //public const string Ctg = "ctg";
        //public const string Cot = "ctg";

        private static readonly HashSet<string> Operators = new()
        {
            Plus, Minus, Multiply, Divide, Power
        };

        private static readonly HashSet<string> Functions = new()
        {
            Pow //Sin, Cos, Tg, Tan, Ctg, Cot
        };

        public static bool IsOperator(string str)           => Operators.Contains(str);
        public static bool IsOperator(char ch)              => IsOperator(ch.ToString());

        public static bool IsFunction(string str)           => Functions.Contains(str);
        public static bool IsFunction(char ch)              => IsFunction(ch.ToString());

        public static bool IsOperatorOrFunction(string str) => Operators.Contains(str) || Functions.Contains(str);
        public static bool IsOperatorOrFunction(char ch)    => IsOperatorOrFunction(ch.ToString());
    }
}
