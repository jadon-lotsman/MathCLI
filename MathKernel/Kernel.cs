using MathCLI.Term.ExpressionBuilder;
using MathCLI.Term.ExpressionStringReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCLI.MathKernel
{
    public class Kernel
    {
        private VariableContext _variableContext;

        public Kernel(VariableContext variableContext)
        {
            _variableContext = variableContext;
        }


        public string Calc(string str)
        {
            string[] splitted = ExpressionStringSplitter.SplitAsExpression(str);

            var buider = new ExpressionBuilder();
            var tree = buider.GetTree(splitted);

            return tree.Execute(_variableContext).ToString();
        }
    }
}
