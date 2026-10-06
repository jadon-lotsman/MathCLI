using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathExpression.ExpressionReader.Enums
{
    public enum TokenType
    {
        Unknown,
        Variable,
        Number,
        Operator,
        Function,
        OpenBracket,
        CloseBracket,
        Comma
    }
}
