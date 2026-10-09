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
        private IReadOnlyList<Token> Tokens;
        private int pos;


        public ITerm CompileDescent(IReadOnlyList<Token> tokens)
        {
            Tokens = tokens;
            pos = 0;

            return GetExpression();
        }

        // E -> T+- ... +-T
        private ITerm GetExpression()
        {
            ITerm a = GetTerm();

            while (!IsOutLength())
            {
                Token tok = Tokens[pos];

                if (tok.IsAnyMatch(Op.Plus, Op.Minus))
                {
                    pos++;
                    ITerm b = GetTerm();

                    if (tok.IsMatch(Op.Plus))
                        a = new Addition(a, b);
                    else
                        a = new Subtraction(a, b);
                }
                else
                {
                    break;
                }
            }

            return a;
        }

        // T -> A*/ ... */A
        private ITerm GetTerm()
        {
            ITerm a = GetUnary();

            while (!IsOutLength())
            {
                Token tok = Tokens[pos];

                if (tok.IsAnyMatch(Op.Multiply, Op.Divide))
                {
                    pos++;
                    ITerm b = GetUnary();

                    if (tok.IsMatch(Op.Multiply))
                        a = new Multiplication(a, b);
                    else
                        a = new Division(a, b);
                }
                else
                {
                    break;
                }
            }

            return a;
        }

        //
        private ITerm GetUnary()
        {
            if (!IsOutLength() && Tokens[pos].IsMatch(Op.Minus))
            {
                pos++;
                return new NegateTerm(GetUnary());
            }

            return GetPower();
        }

        // P -> abc(E, E ... ,E) ^ P
        private ITerm GetPower()
        {
            ITerm a = GetAbc();

            if (!IsOutLength() && Tokens[pos].IsMatch(Op.Power))
            {
                pos++;
                var b = GetUnary();
                return new Power([a, b]);
            }

            return a;
        }

        // P -> abc(E, E ... ,E)
        private ITerm GetAbc()
        {
            // Get abc token
            Token abc = Tokens[pos];
            if (abc.Kind != TokenType.Function)
                return GetFactor();

            // Move through open bracket
            Token open = Tokens[++pos];
            if (open.Kind != TokenType.OpenBracket)
                throw new Exception("Need bracket after function");
            pos++;

            // Get fucntion arguments
            ITerm[] args = GetCommaArgs();

            // Move through close bracket
            Token? close = !IsOutLength() ? Tokens[pos] : null;
            if (!close.HasValue || close.Value.Kind != TokenType.CloseBracket)
                throw new Exception("Need close bracket");
            pos++;

            // Match functions
            if (abc.IsMatch(Op.Pow))
                return new Power(args);

            throw new Exception("Not found function");
        }

        // F -> N | (E)
        private ITerm GetFactor()
        {
            Token factor = Tokens[pos];
            ITerm result;

            if (factor.Kind == TokenType.OpenBracket)
            {
                pos++;
                result = GetExpression();
                Token closingBracket;
                if (!IsOutLength())
                {
                    closingBracket = Tokens[pos];
                }
                else
                {
                    throw new Exception("No end expression");
                }

                if (IsOutLength() || closingBracket.Kind != TokenType.CloseBracket)
                    throw new Exception("End is not a bracket");

                pos++;
            }
            else if (factor.Kind == TokenType.Variable)
            {
                result = new Variable(factor.Value[0]);
                pos++;
            }
            else
            {
                if (!Fraction.TryParseFraction(factor.Value, out var fraction))
                    throw new Exception("Invalid fraction format");

                result = fraction;
                pos++;
            }

            return result;
        }

        private bool IsOutLength()
        {
            return pos > Tokens.Count - 1;
        }

        private ITerm[] GetCommaArgs()
        {
            var args = new List<ITerm>();

            if (!IsOutLength() && Tokens[pos].Kind == TokenType.CloseBracket)
                return args.ToArray();

            args.Add(GetExpression());

            while (!IsOutLength() && Tokens[pos].Kind == TokenType.Comma)
            {
                pos++;

                if (IsOutLength())
                    throw new Exception("Expect comma");

                args.Add(GetExpression());
            }

            return args.ToArray();
        }
    }
}
