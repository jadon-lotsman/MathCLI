using MathCLI.Extensions;
using MathCLI.MathKernel;
using MathCLI.MathKernel.Numbers;
using MathCLI.MathKernel.Terms;
using MathCLI.MathKernel.Variables;
using MathCLI.MathNumerics;
using MathCLI.MathSyntax.ExpressionReader;
using MathCLI.MathSyntax.ExpressionReader.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathSyntax.ExpressionCompiler
{
    public class ExpressionCompiler
    {
        private int pos;
        private ReadResult read;


        public Term CompileDescent(ReadResult readResult)
        {
            pos = 0;
            read = readResult;
            var term = GetExpression();

            if (pos != read.Tokens.Count)
                throw new Exception($"Unexpected token '{read.TextOf(pos).ToString()}' at {pos}");

            return term;
        }

        // E -> T+- ... +-T
        private Term GetExpression()
        {
            Term a = GetTerm();

            while (TryPeek(out Token expToken) && expToken.IsAnyMatch(TokenType.Sum, TokenType.Sub))
            {
                Advance();
                Term b = GetTerm();

                a = expToken.IsMatch(TokenType.Sum)
                    ? new Addition(a, b)
                    : new Subtraction(a, b);
            }

            return a;
        }

        // T -> A*/ ... */A
        private Term GetTerm()
        {
            Term a = GetUnary();

            while (TryPeek(out Token termToken) && termToken.IsAnyMatch(TokenType.Mul, TokenType.Div))
            {
                Advance();
                Term b = GetUnary();

                a = termToken.IsMatch(TokenType.Mul)
                    ? new Multiplication(a, b)
                    : new Division(a, b);
            }

            return a;
        }

        // U -> -U | P
        private Term GetUnary()
        {
            if (TryPeek(out Token unarToken) && unarToken.IsMatch(TokenType.Sub))
            {
                Advance();
                return new NegateTerm(GetUnary());
            }

            return GetPower();
        }

        // P -> abcToken(E, E, ...) ^ P
        private Term GetPower()
        {
            Term a = GetAbc();

            if (TryPeek(out Token powToken) && powToken.IsMatch(TokenType.Pow))
            {
                Advance();
                Term b = GetUnary();
                return new Power(a, b);
            }

            return a;
        }

        // A -> abc(E, E, ...) | F
        private Term GetAbc()
        {
            // Get abcToken token
            if (!TryPeek(out Token abcToken) || !abcToken.IsFunction())
                return GetFactor();

            // Capture func
            Advance();

            // Move through open bracket
            if (!TryAdvance(out Token open) || open.Type != TokenType.OpenBracket)
                throw new Exception("Need bracket after function");

            // Get function arguments
            Term[] args = GetCommaArgs();

            // Move through close bracket
            if (!TryAdvance(out Token close) || close.Type != TokenType.CloseBracket)
                throw new Exception("Need close bracket");

            // Match functions
            if (abcToken.IsMatch(TokenType.Powf))
                return new Power(args);

            throw new Exception("Not found function");
        }

        // F -> N | V | (E)
        private Term GetFactor()
        {
            if (!TryPeek(out Token factorToken))
                throw new Exception("Unexpected end of expression");

            Term factor;
            switch (factorToken.Type)
            {
                case TokenType.OpenBracket:

                    Advance();
                    factor = GetExpression();

                    if (!TryAdvance(out Token closingBracket) || !closingBracket.IsMatch(TokenType.CloseBracket))
                        throw new Exception("End is not a bracket");

                    break;

                case TokenType.Variable:

                    if (!TryPeekValue(out var name))
                        throw new Exception("Invalid variable name format");

                    factor = new Variable(name[0]);
                    Advance();
                    break;

                case TokenType.Number:

                    if (!TryPeekValue(out var value) || !Fraction.TryParseFraction(value, out var fraction))
                        throw new Exception("Invalid fraction format");

                    factor = new Number(fraction!);
                    Advance();
                    break;

                default:
                    throw new Exception("Unknown token exeption");
            }

            return factor;
        }

        private Term[] GetCommaArgs()
        {
            var args = new List<Term>();

            if (TryPeek(out Token first) && first.IsMatch(TokenType.CloseBracket))
                return args.ToArray();

            args.Add(GetExpression());

            while (TryPeek(out Token tok) && tok.IsMatch(TokenType.Comma))
            {
                Advance();

                if (IsOutLength())
                    throw new Exception("Expect comma");

                args.Add(GetExpression());
            }

            return args.ToArray();
        }


        private bool IsOutLength() => pos >= read.Tokens.Count;
        private Token Advance() => read.Tokens[pos++];

        private bool TryPeekValue(out ReadOnlySpan<char> span)
        {
            if (IsOutLength())
            {
                span = default;
                return false;
            }

            var text = read.TextOf(pos);
            if (text.IsEmpty)
            {
                span = default;
                return false;
            }

            span = text;
            return true;
        }

        private bool TryPeek(out Token token)
        {
            if (IsOutLength())
            {
                token = default;
                return false;
            }
            token = read.Tokens[pos];
            return true;
        }

        private bool TryAdvance(out Token token)
        {
            if (IsOutLength())
            {
                token = default;
                return false;
            }
            token = Advance();
            return true;
        }
    }
}
