using MathCLI.MathExpression.ExpressionCompiler;
using MathCLI.MathExpression.ExpressionReader;
using MathCLI.MathKernel;

namespace MathCLI.Tests
{
    public class ExpressionCompilerTest
    {
        public static TheoryData<string, double> MathCases => new()
        {
            { "2+2*2", 6 },
            { "2/2/2", 0.5 },
            { "10-5-2", 3 },
            { "100/10/2", 5 },
            { "8*4/2", 16 },
            { "8/4*2", 4 },
            { "2+3*4", 14 },
            { "10-6/2", 7 },
            { "(2+3)*4", 20 },
            { "2*(3+4)", 14 },
            { "((2+3)*4)-5", 15 },
            { "((2+3)*(4-1))/5", 3 },
            { "-2+3", 1 },
            { "-(2+3)", -5 },
            { "-2*3", -6 },
            //{ "--2", 2 }
            { "5 - -3", 8 },
            //{ "10/(2+3)", 2 },
            { "1+2*3-4/2+5", 10 },
            { "2 + 3 * 4 - 5 / 5", 13 },
            { "3 - (2 - 1)", 2 },
            { "2 * 3 * 4", 24 },
            { "10 / 2 / 5", 1 },
            { "(2+2)*(2+2)", 16 },
            { "2 + (3 * (4 - 1))", 11 },
            { "((2))", 2 },
            { "-(-2)", 2 },
            { "1 - 2 - 3 - 4", -8 },
            { "100 / 10 / 5 / 2", 1 }
        };

        [Theory]
        [MemberData(nameof(MathCases))]
        public void Parse_And_Evaluate_ReturnsExpected(string expression, double expected)
        {
            var readed = ExpressionReader.Read(expression);
            Assert.Empty(readed.UnknownTokens);

            var variables = new VariableContext();
            var compiler = new ExpressionCompiler();
            var actual = compiler.CompileDescent(readed.Tokens).Execute(variables);

            Assert.Equal(expected, actual, precision: 5);
        }
    }
}