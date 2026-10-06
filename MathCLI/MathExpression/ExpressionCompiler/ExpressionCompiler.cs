using MathCLI.Extensions;
using MathCLI.MathExpression;
using MathCLI.MathExpression.ExpressionReader;
using MathCLI.MathExpression.ExpressionReader.Enums;
using MathCLI.MathExpression.Terms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathExpression.ExpressionCompiler
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
            ITerm a = GetPower();

            while (!IsOutLength())
            {
                Token tok = Tokens[pos];

                if (tok.IsAnyMatch(Op.Multiply, Op.Divide))
                {
                    pos++;
                    ITerm b = GetPower();

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

        // P -> abc(E, E ... ,E) ^ P
        private ITerm GetPower()
        {
            ITerm a = GetAbc();

            while (!IsOutLength())
            {
                Token tok = Tokens[pos];

                if (tok.IsAnyMatch(Op.Power))
                {
                    pos++;
                    ITerm b = GetPower();

                    if (tok.IsMatch(Op.Power))
                        a = new Power(a, b);
                }
                else
                {
                    break;
                }
            }

            return a;
        }


        // P -> abc(E, E ... ,E)
        private ITerm GetAbc()
        {
            // Get token
            Token current = Tokens[pos];
            if (current.Kind != TokenType.Function)
                return GetFactor();

            // Move to open bracket
            Token open = Tokens[++pos];
            if (open.Kind != TokenType.OpenBracket)
                throw new Exception("Need bracket after function");

            pos++;
            ITerm[] args = GetArgs();

            Token? close = !IsOutLength() ? Tokens[pos] : null;
            if (!close.HasValue || close.Value.Kind != TokenType.CloseBracket)
                throw new Exception("Need close bracket");

            pos++;


            if (current.IsMatch(Op.Pow))
                return new Power(args[0], args[1]);

            throw new Exception("Not found function");
        }

        // F -> N | (E)
        private ITerm GetFactor()
        {
            bool IsMinus = false;

            Token tok = Tokens[pos];
            if (tok.IsMatch(Op.Minus))
            {
                IsMinus = true;
                pos++;
            }

            Token next = Tokens[pos];
            ITerm result;

            if (next.Kind == TokenType.OpenBracket)
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
                {
                    throw new Exception("End is not a bracket");
                }
            }
            else if (next.Kind == TokenType.Variable)
            {
                result = new Variable(next.Value[0]);
            }
            else
            {
                if (!Fraction.TryParseFraction(next.Value, out var fraction))
                    throw new Exception("Invalid fraction format");

                result = fraction;
            }

            pos++;

            if (IsMinus)
                return new Multiplication(result, new Fraction(-1));

            return result;
        }

        private bool IsOutLength()
        {
            return pos > Tokens.Count - 1;
        }

        private ITerm[] GetArgs()
        {
            List<ITerm> args = new List<ITerm>();

            while (!IsOutLength())
            {
                args.Add(GetExpression());

                Token next;
                if (!IsOutLength())
                {
                    next = Tokens[pos];
                }
                else
                {
                    break;
                }

                if (!IsOutLength() && next.Kind == TokenType.Comma)
                {
                    pos++;
                }
                else
                {
                    break;
                }
            }

            return args.ToArray();
        }
    }
}
