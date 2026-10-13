using MathCLI.MathKernel.Variables;
using MathCLI.MathKernel.TermVisitors;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel
{
    public abstract class FunctionTerm : ITerm
    {
        protected string Name { get; }
        public ITerm[] Args { get; }
        public int MinArgs { get; }
        public int? MaxArgs { get; }
        public int Precedence { get => int.MaxValue; }
        public bool IsValue { get => false; }


        public FunctionTerm(string name, ITerm[] args, int minArgs, int? maxArgs=null)
        {
            Name = name;
            Args = args;
            MinArgs = minArgs;
            MaxArgs = maxArgs ?? minArgs;
        }


        public abstract ITerm AcceptFunction(ITermVisitor visitor);
        public ITerm Accept(ITermVisitor visitor)
        {
            if (Args.Length < MinArgs || Args.Length > MaxArgs)
            {
                var requires = DescribeArity(MinArgs, MaxArgs);
                throw new Exception($"Function '{Name}' expects {requires}, but got {Args.Length}!");
            }

            return AcceptFunction(visitor);
        }


        public override string ToString()
        {
            var argStr = string.Join(", ", (object[])Args);
            return $"{Name}({argStr})";
        }

        private string DescribeArity(int min, int? max)
        {
            if (max is null)
                return min == 1
                    ? "at least 1 argument"
                    : $"at least {min} arguments";

            if (min == max)
                return min switch
                {
                    0 => "no arguments",
                    1 => "exactly 1 argument",
                    _ => $"exactly {min} arguments"
                };

            return $"between {min} and {max} arguments";
        }
    }
}
