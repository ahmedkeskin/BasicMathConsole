using System;
using System.Collections.Generic;
using System.Text;

namespace BasicMathConsole.Operations
{
    public class Sum : Operation
    {
        public Sum()
        {
            Sign = "+";
            Name = "Sum";
        }
        public override float Calculate(float number1, float number2)
        {
            return number1 + number2;
        }
    }
}
