using MathCLI.MathKernel;

namespace MathCLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var str = string.Join("", args);

            var variables = new VariableContext();
            var kernel = new Kernel(variables);

            str = "2+2*2";
            string result = kernel.Calc(str);

            Console.WriteLine(result);
            Console.ReadLine();
        }
    }
}
