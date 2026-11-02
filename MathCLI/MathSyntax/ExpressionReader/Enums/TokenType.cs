using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathSyntax.ExpressionReader.Enums
{
    public enum TokenType
    {
        BitMask     = 0xFF00,

        Unknown     = -1,
        Number      = 0,
        Variable    = 1,


        Punctuation = 0x0100,
        Comma       = Punctuation | 1,
        OpenBracket = Punctuation | 2,
        CloseBracket= Punctuation | 3,


        Operator    = 0x0200,
        Sum         = Operator | 1,
        Sub         = Operator | 2,
        Mul         = Operator | 3,
        Div         = Operator | 4,
        Pow         = Operator | 5,


        Function    = 0x0300,
        Powf        = Function | 1,
        Sqrt        = Function | 2,
    }
}
