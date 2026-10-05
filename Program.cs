using MathCLI.MathKernel;
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

            RootCommand rootCommand = new RootCommand()
            {
                expressionArgument,
                variablesOption
            };


            rootCommand.SetAction(parseResult =>
            {
                var variables = new VariableContext();

                string expression = parseResult.GetValue(expressionArgument);
                string[] variablePairs = parseResult.GetValue(variablesOption) ?? Array.Empty<string>();

                var parameters = new Dictionary<string, object>();
                foreach (var pair in variablePairs)
                {
                    var parts = pair.Split('=', 2);
                    if (parts.Length != 2)
                    {
                        Console.Error.WriteLine($"Invalid variable format: requests name=value");
                        return 2;
                    }

                    variables.SetVariable(parts[0][0], double.Parse(parts[1].Trim()));
                }


                var kernel = new Kernel(variables);
                string result = kernel.Calc(expression);
                Console.WriteLine(result);

                return 0;
            });


            return await rootCommand.Parse(args).InvokeAsync();
        }
    }
}
