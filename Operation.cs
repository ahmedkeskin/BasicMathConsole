using System;
using System.Collections.Generic;
using System.Text;

namespace BasicMathConsole
{
    public abstract class Operation
    {
        public string Name { get; set; }
        public string Sign { get; set; }
        public abstract float Calculate(float number1, float number2);
    }
}
