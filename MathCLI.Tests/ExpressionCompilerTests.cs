using MathCLI.MathKernel.Variables;
using MathCLI.MathSyntax.ExpressionCompiler;
using MathCLI.MathSyntax.ExpressionReader;

namespace MathCLI.Tests
{
    public class ExpressionCompilerTests
    {
        public static TheoryData<string, double> MathCases => new()
        {
            { "2+2*2", 6 },
            { "2*2+2", 6 },
            { "2+2/2", 3 },
            { "2/2+2", 3 },
            { "2+2^2", 6 },
            { "2^2+2", 6 },
            { "2*2^3", 16 },
            { "2^2*3", 12 },
            { "10-2*3", 4 },
            { "10-2^3", 2 },
            { "2+3*4-5", 9 },
            { "(2+3)*4", 20 },
            { "2*(3+4)", 14 },
            { "2*(3+4)^2", 98 },
            { "(2+3)^2", 25 },
            { "((2+3)*4)^2", 400 },
            { "100/(5*2)", 10 },
            { "100/5*2", 40 },
            { "100/(5/2)", 40 },
            { "2^3^2", 512 },
            { "pow(2,3)", 8 },
            { "pow(2,3)+1", 9 },
            { "pow(2+1,2)", 9 },
            { "pow(2, pow(2,3))", 256 },
            { "pow(2,3^2)", 512 },
            { "-2+3", 1 },
            { "-(2+3)", -5 },
            { "-2*3", -6 },
            { "2*-3", -6 },
            { "2*(-3)", -6 },
            { "-2^2", -4 },
            { "(-2)^2", 4 },
            { "-2^3", -8 },
            { "(-2)^3", -8 },
            { "2^-2", 0.25 },
            { "(-2)^-2", 0.25 },
            { "-(-5)", 5 },
            { "3--2", 5 },
            { "3+-2", 1 },
            { "3*-2", -6 },
            { "3/-2", -1.5 },
            { "-3/2", -1.5 },
            { "-3/-2", 1.5 },
            { "2/4", 0.5 },
            { "2 / 4", 0.5 },
            { "1/2+1/4", 0.75 },
            { "(1/2)/(1/4)", 2 },
            { "2/4*2", 1 },
            { "2 / 4 * 2", 1 },
            { "8/2/2", 2 },
            { "1+2/4", 1.5 },
            { "1 + 2 / 4", 1.5 },
            { "3+4*5", 23 },
            { "10-3*2", 4 },
            { "(10-3)*2", 14 },
            { "2^(3+1)", 16 },
            { "pow(3, 2) - 1", 8 },
            { "pow(2+2, 2)", 16 },
            { "-(5+3)", -8 },
            { "--5", 5 },
            { "-3*-2", 6 },
            { "10 / -2", -5 },
            { "2*3^2", 18 },
            { "(2*3)^2", 36 },
            { "100 / 10 / 2", 5 },
            { "1-2+3-4", -2 },
            { "2*(3+4)-5", 9 },
            { "((2+3)*2)^2", 100 },
            { "pow(2, pow(2, 2))", 16 },
            { "pow(2,3)^2", 64 },
            { "(2^3)^2", 64 },
            { "3 + 4 / 2 * 2", 7 },
            { "((1+2)*(3+4))/3", 7 },
            { "((2+3)*4)-5", 15 },
            { "((2+3)*(4-1))/5", 3 },
            { "-2 + --2", 0 },
            { "5 - -3", 8 },
            { "10/(2+3)", 2 },
            { "2 + (3 * (4 - 1))", 11 },
            { "(2+2)*(2+2)", 16 },
        };

        [Theory]
        [MemberData(nameof(MathCases))]
        public void ReadAndExecute_ReturnsExpected(string expression, double expected)
        {
            var reader = new ExpressionReader();
            var context = new VariableContext();
            var compiler = new ExpressionCompiler();

            var read = reader.Read(expression);
            Assert.Empty(read.UnknownTokens);

            var actual = compiler.CompileDescent(read.Tokens).Execute(context);
            Assert.Equal(expected, actual, precision: 5);
        }
    }
}