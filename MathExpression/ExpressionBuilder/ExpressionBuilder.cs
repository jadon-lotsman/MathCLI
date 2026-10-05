using MathCLI.Extensions;
using MathCLI.MathExpression;
using MathCLI.MathExpression.Terms;
using MathCLI.MathTerm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.Term.ExpressionBuilder
{
    public class ExpressionBuilder
    {
        private string[] tokens;
        private int pos;

        public ExpressionBuilder()
        {

        }

        public ITerm GetTree(string[] splittedStr)
        {
            pos = 0;
            tokens = splittedStr;
            ITerm tree = GetExpression();

            return tree;
        }

        // E -> T+- ... +-T
        private ITerm GetExpression()
        {
            ITerm a = GetTerm();

            while (!IsOutLength())
            {
                string oper = tokens[pos];

                if (oper == "+" || oper == "-")
                {
                    pos++;
                }
                else
                {
                    break;
                }

                ITerm b = GetTerm();

                if (oper == "+")
                {
                    a = new Addition(a, b);
                }
                else
                {
                    a = new Subtraction(a, b);
                }
            }

            return a;
        }

        // T -> A*/ ... */A
        private ITerm GetTerm()
        {
            ITerm a = GetAbc();

            while (!IsOutLength())
            {
                string oper = tokens[pos];

                if (oper == "*" || oper == ":")
                {
                    pos++;
                }
                else
                {
                    break;
                }

                ITerm b = GetAbc();

                if (oper == "*")
                {
                    a = new Multiplication(a, b);
                }
                else
                {
                    a = new Division(a, b);
                }
            }

            return a;
        }

        // A -> abc(E,E ... ,E)
        private ITerm GetAbc()
        {
            string func = tokens[pos];

            if (func == "pow" || func == "sqrt" || func == "max" || func == "min")
            {
                pos++;
            }
            else
            {
                return GetFactor();
            }

            string next = tokens[pos];

            if (next == "(")
            {
                pos++;
                ITerm[] args = GetArgs();

                string closingBracket;
                if (!IsOutLength())
                {
                    closingBracket = tokens[pos];
                }
                else
                {
                    throw new Exception("No end expression");
                }

                if (closingBracket == ")")
                {
                    pos++;

                    //if (func == "pow")
                    //{
                    //    return new Power(args[0], args[1]);
                    //}
                    //else if (func == "sqrt")
                    //{
                    //    return new SquareRoot(args[0]);
                    //}
                    //else if (func == "max")
                    //{
                    //    return new Maximal(args);
                    //}
                    //else if (func == "min")
                    //{
                    //    return new Minimal(args);
                    //}
                }
                else
                {
                    throw new Exception("End is not a bracket");
                }
            }

            throw new Exception("Need bracket after function");
        }

        // F -> N | (E)
        private ITerm GetFactor()
        {
            bool IsMinus = false;

            if (tokens[pos] == "-")
            {
                IsMinus = true;
                pos++;
            }

            string next = tokens[pos];
            ITerm result;

            if (next == "(")
            {
                pos++;

                result = GetExpression();
                string closingBracket;
                if (!IsOutLength())
                {
                    closingBracket = tokens[pos];
                }
                else
                {
                    throw new Exception("No end expression");
                }

                if (IsOutLength() || closingBracket != ")")
                {
                    throw new Exception("End is not a bracket");
                }
            }
            else if (next.Length == 1 && next[0].isLetter())
            {
                result = new Variable(next[0]);
            }
            else
            {
                string[] splitted = next.Split('/');
                int numerator = Convert.ToInt32(splitted[0]);
                int denominator = Convert.ToInt32(splitted[1]);

                result = new Fraction(numerator, denominator);
            }
            pos++;

            return SetUnarMinus(result, IsMinus);
        }

        private bool IsOutLength()
        {
            return pos > tokens.Length - 1;
        }

        private ITerm SetUnarMinus(ITerm exp, bool IsMinus)
        {
            if (IsMinus)
                return new Multiplication(exp, new Fraction(-1));

            return exp;
        }

        private ITerm[] GetArgs()
        {
            List<ITerm> args = new List<ITerm>();

            while (!IsOutLength())
            {
                args.Add(GetExpression());

                string next;
                if (!IsOutLength())
                {
                    next = tokens[pos];
                }
                else
                {
                    break;
                }

                if (!IsOutLength() && next == ",")
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
