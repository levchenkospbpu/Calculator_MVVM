using System;
using System.Collections.Generic;

namespace Calculator_MVVM
{
    public class PostfixEvaluator
    {
        Stack<Element> _stack = new Stack<Element>();

        public NumberElement Calculate(NumberElement left, NumberElement right, OperatorElement operatorElement)
        {
            double temp = double.MaxValue;
            switch(operatorElement.Type)
            {
                case OperatorType.ROOT:
                    temp = Math.Pow(right.Number, 1 / left.Number);
                    break;
                case OperatorType.ADD:
                    temp = left.Number + right.Number;
                    break;
                case OperatorType.SUBTRACT:
                    temp = left.Number - right.Number;
                    break;
                case OperatorType.MULTIPLY:
                    temp = left.Number * right.Number;
                    break;
                case OperatorType.DIVIDE:
                    temp = left.Number / right.Number;
                    break;
                case OperatorType.EXPONENTIAL:
                    temp = Math.Pow(left.Number, right.Number);
                    break;
            }
            return new NumberElement(temp.ToString());
        }

        public double Evaluate(List<Element> elements)
        {
            List<Element> e = new List<Element>(elements);
            for (int i = 0; i < e.Count; i++)
            {
                Element element = e[i];
                if (element.GetType().Equals(typeof(NumberElement)))
                {
                    _stack.Push(element);
                }
                if (element.GetType().Equals(typeof(OperatorElement)))
                {
                    NumberElement right = (NumberElement)_stack.Pop();
                    NumberElement left = (NumberElement)_stack.Pop();
                    NumberElement result = Calculate(left, right, (OperatorElement)element);
                    _stack.Push(result);
                }
            }
            return ((NumberElement)_stack.Pop()).Number;
        }
    }
}
