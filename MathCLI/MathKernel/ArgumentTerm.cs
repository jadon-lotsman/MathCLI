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
    public abstract class ArgumentTerm : ITerm
    {
        public string Name { get; }
        public ITerm[] Args { get; }
        public int MinArgs { get; }
        public int? MaxArgs { get; }
        public abstract bool IsInfix { get; }
        public abstract bool IsRightAssoc { get; }
        public abstract int Precedence { get; }


        public ArgumentTerm(string name, ITerm[] args, int minArgs) : this(name, args, minArgs, minArgs) { }

        public ArgumentTerm(string name, ITerm[] args, int minArgs, int? maxArgs)
        {
            if (IsInfix && args.Length < 2)
                throw new InvalidOperationException($"Infix term '{name}' requires at least 2 args, got {args.Length}");

            if (args.Length < minArgs || (maxArgs.HasValue && args.Length > maxArgs.Value))
                throw new ArgumentException($"Term '{name}' expects {DescribeArity(minArgs, maxArgs)}, got {args.Length}");

            Name = name;
            Args = args;
            MinArgs = minArgs;
            MaxArgs = maxArgs;
        }


        public abstract T Accept<T>(ITermVisitor<T> visitor);

        public override string ToString()
        {
            if (!Args.Any())
                return Name;

            if (IsInfix)
                return "(" + string.Join($"{Name}", Args.Select(a => a.ToString())) + ")";

            return $"{Name}({string.Join(", ", Args.Select(a => a.ToString()))})";
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
