using MathCLI.Extensions;
using MathCLI.MathSyntax.ExpressionReader;
using MathCLI.MathSyntax.ExpressionReader.Enums;
using MathCLI.MathKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MathCLI.MathKernel.Terms;
using MathCLI.MathKernel.Variables;

namespace MathCLI.MathSyntax.ExpressionCompiler
{
    public class ExpressionCompiler
    {
        private int pos;
        private IReadOnlyList<Token> tokens;


        public ITerm CompileDescent(IReadOnlyList<Token> tokens)
        {
            pos = 0;
            this.tokens = tokens;

            return GetExpression();
        }

        // E -> T+- ... +-T
        private ITerm GetExpression()
        {
            ITerm a = GetTerm();

            while (TryPeek(out Token expToken) && expToken.IsAnyMatch(Op.Plus, Op.Minus))
            {
                Advance();
                ITerm b = GetTerm();

                a = expToken.IsMatch(Op.Plus)
                    ? new Addition(a, b)
                    : new Subtraction(a, b);
            }

            return a;
        }

        // T -> A*/ ... */A
        private ITerm GetTerm()
        {
            ITerm a = GetUnary();

            while (TryPeek(out Token termToken) && termToken.IsAnyMatch(Op.Multiply, Op.Divide))
            {
                Advance();
                ITerm b = GetUnary();

                a = termToken.IsMatch(Op.Multiply)
                    ? new Multiplication(a, b)
                    : new Division(a, b);
            }

            return a;
        }

        // U -> -U | P
        private ITerm GetUnary()
        {
            if (TryPeek(out Token unarToken) && unarToken.IsMatch(Op.Minus))
            {
                Advance();
                return new NegateTerm(GetUnary());
            }

            return GetPower();
        }

        // P -> abcToken(E, E, ...) ^ P
        private ITerm GetPower()
        {
            ITerm a = GetAbc();

            if (TryPeek(out Token powToken) && powToken.IsMatch(Op.Power))
            {
                Advance();
                ITerm b = GetUnary();
                return new Power([a, b]);
            }

            return a;
        }

        // A -> abc(E, E, ...) | F
        private ITerm GetAbc()
        {
            // Get abcToken token
            if (!TryPeek(out Token abcToken) || abcToken.Kind != TokenType.Function)
                return GetFactor();

            // Capture func
            Advance();

            // Move through open bracket
            if (!TryAdvance(out Token open) || open.Kind != TokenType.OpenBracket)
                throw new Exception("Need bracket after function");

            // Get function arguments
            ITerm[] args = GetCommaArgs();

            // Move through close bracket
            if (!TryAdvance(out Token close) || close.Kind != TokenType.CloseBracket)
                throw new Exception("Need close bracket");

            // Match functions
            if (abcToken.IsMatch(Op.Pow))
                return new Power(args);

            throw new Exception("Not found function");
        }

        // F -> N | V | (E)
        private ITerm GetFactor()
        {
            if (!TryPeek(out Token factorToken))
                throw new Exception("Unexpected end of expression");

            ITerm factor;
            switch (factorToken.Kind)
            {
                case TokenType.OpenBracket:
                    Advance();
                    factor = GetExpression();

                    if (!TryAdvance(out Token closingBracket) || closingBracket.Kind != TokenType.CloseBracket)
                        throw new Exception("End is not a bracket");

                    break;

                case TokenType.Variable:
                    factor = new Variable(factorToken.Value[0]);
                    Advance();
                    break;

                case TokenType.Number:

                    if (!Fraction.TryParseFraction(factorToken.Value, out var fraction))
                        throw new Exception("Invalid fraction format");

                    factor = fraction;
                    Advance();
                    break;

                default:
                    throw new Exception("Unknown token exeption");
            }

            return factor;
        }

        private ITerm[] GetCommaArgs()
        {
            var args = new List<ITerm>();

            if (TryPeek(out Token first) && first.Kind == TokenType.CloseBracket)
                return args.ToArray();

            args.Add(GetExpression());

            while (TryPeek(out Token tok) && tok.Kind == TokenType.Comma)
            {
                Advance();

                if (IsOutLength())
                    throw new Exception("Expect comma");

                args.Add(GetExpression());
            }

            return args.ToArray();
        }


        private bool IsOutLength() => pos >= tokens.Count;
        private Token Advance() => tokens[pos++];

        private bool TryPeek(out Token token)
        {
            if (IsOutLength())
            {
                token = default;
                return false;
            }
            token = tokens[pos];
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
