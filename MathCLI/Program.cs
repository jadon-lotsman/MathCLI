using MathCLI.MathKernel.Variables;
using MathCLI.MathSolver;
using MathCLI.MathSyntax.ExpressionReader;
using MathCLI.MathSyntax.ExpressionCompiler;
using System.CommandLine;

namespace MathCLI
{
    internal class Program
    {
        static async Task<int> Main(string[] args)
        {
            var expressionArgument = new Argument<string>("expression")
            {
                Description = "Your math expression",
                Arity = ArgumentArity.ExactlyOne
            };

            var variablesOption = new Option<string[]>("--variable", "-var")
            {
                Description = "A variable value in format 'name=value'",
                Arity = ArgumentArity.ZeroOrMore
            };

            var stepsOption = new Option<bool>("--steps", "-s")
            {
                Description = "Detailed solution",
                Arity = ArgumentArity.ZeroOrOne
            };

            RootCommand rootCommand = new RootCommand()
            {
                expressionArgument,
                variablesOption,
                stepsOption
            };


            rootCommand.SetAction(p =>
            {
                var solver  = new ExpressionSolver(new ExpressionCompiler(), new ExpressionReader());
                var context = new VariableContext();

                var expression  = p.GetValue(expressionArgument);
                var variables   = p.GetValue(variablesOption) ?? Array.Empty<string>();
                var step        = p.GetValue(stepsOption) ? SolverMode.StepByStep : SolverMode.Value;

                foreach (var pair in variables)
                {
                    var parts = pair.Split('=', 2);
                    if (parts.Length != 2)
                    {
                        Console.Error.WriteLine($"Invalid variable format: requests name=value");
                        return 2;
                    }

                    context.SetVariable(parts[0][0], double.Parse(parts[1].Trim()));
                }


                var result = solver.Solve(expression, context, step);
                foreach(var res in result)
                {
                    Console.WriteLine(res);
                }

                return 0;
            });


            return await rootCommand.Parse(args).InvokeAsync();
        }
    }
}
