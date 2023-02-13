using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculator_MVVM
{
    public class InfixToPostfix
    {
        private List<Element> _converted = new List<Element>();

        int Precedence(OperatorElement operatorElement)
        {
            switch(operatorElement.Type)
            {
                case OperatorType.ROOT:
                case OperatorType.EXPONENTIAL:
                    return 2;
                case OperatorType.MULTIPLY:
                case OperatorType.DIVIDE:
                    return 3;
                case OperatorType.ADD:
                case OperatorType.SUBTRACT:
                    return 4;
                default:
                    return int.MaxValue;
            }
        }

        public void ProcessOperators(Stack<Element> stack, Element element, Element topElement)
        {
            while (stack.Count > 0 && Precedence((OperatorElement)element) >= Precedence((OperatorElement)topElement))
            {
                Element p = stack.Pop();
                if (((OperatorElement)p).Type == OperatorType.OPENBRACKET)
                {
                    break;
                }
                _converted.Add(p);
                if (stack.Count > 0)
                {
                    topElement = stack.First();
                }
            }
        }

        public List<Element> ConvertFromInfixToPostFix(List<Element> elements)
        {
            List<Element> e = new List<Element>(elements);
            Stack<Element> st = new Stack<Element>();
            for (int i = 0; i < e.Count; i++)
            {
                Element element = e[i];
                if (element.GetType().Equals(typeof(OperatorElement)))
                {
                    if (st.Count == 0 || ((OperatorElement)element).Type == OperatorType.OPENBRACKET)
                    {
                        st.Push(element);
                    }
                    else
                    {
                        Element top = st.First();
                        if (((OperatorElement)element).Type == OperatorType.CLOSEBRACKET)
                        {
                            ProcessOperators(st, element, top);
                        }
                        else if (Precedence((OperatorElement)element) < Precedence((OperatorElement)top))
                        {
                            st.Push(element);
                        }
                        else
                        {
                            ProcessOperators(st, element, top);
                            st.Push(element);
                        }
                    }
                }
                else
                {
                    _converted.Add(element);
                }
            }

            while (st.Count > 0)
            {
                Element b1 = st.Pop();
                _converted.Add(b1);
            }

            return _converted;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int j = 0; j < _converted.Count; j++)
            {
                sb.Append(_converted[j].ToString() + " ");
            }
            return sb.ToString();
        }
    }
}
