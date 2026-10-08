using MathCLI.MathKernel.Variables;
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
        protected ITerm[] Args;
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


        public abstract Fraction ExecuteFunction(VariableContext context);

        public Fraction Execute(VariableContext context)
        {
            if (Args.Length < MinArgs || Args.Length > MaxArgs)
            {
                var requires = DescribeArity(MinArgs, MaxArgs);
                throw new Exception($"Function '{Name}' expects {requires}, but got {Args.Length}!");
            }

            return ExecuteFunction(context);
        }

        public ITerm ReduceStep(VariableContext context)
        {
            if (Args.Any() && Args.All(e => e.IsValue))
                return Execute(context);

            for (int i = 0; i < Args.Length; i++)
            {
                if (!Args[i].IsValue)
                {
                    Args[i] = Args[i].ReduceStep(context);
                    return this;
                }
            }

            return this;
        }

        public ITerm Substitute(VariableContext context)
        {
            for (int i = 0; i < Args.Length; i++)
                Args[i] = Args[i].Substitute(context);
                
            return this;
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
