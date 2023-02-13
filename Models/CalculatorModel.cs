using System;
using System.Collections.Generic;

namespace Calculator_MVVM
{
    public class CalculatorModel
    {
        public Tuple<string, double> Calculate(string infixExpression)
        {
            try
            {
                Parser parser = new Parser();
                List<Element> elements = parser.Parse(infixExpression);
                InfixToPostfix i2p = new InfixToPostfix();
                elements = i2p.ConvertFromInfixToPostFix(elements);
                string elementsString = string.Empty;
                foreach (Element e in elements)
                {
                    elementsString += e.ToString() + " ";
                }

                PostfixEvaluator postfixEvaluator = new PostfixEvaluator();
                double result = postfixEvaluator.Evaluate(elements);
                return new Tuple<string, double>(elementsString, result);
            }
            catch (Exception)
            {
                return new Tuple<string, double>("Incorrect expression", 0);
            }
        }
    }

    public enum OperatorType
    {
        MULTIPLY,
        DIVIDE,
        ADD,
        SUBTRACT,
        EXPONENTIAL,
        ROOT,
        OPENBRACKET,
        CLOSEBRACKET
    };
}
