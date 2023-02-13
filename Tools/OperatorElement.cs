using System;
using System.Collections.Generic;
namespace Calculator_MVVM
{
    public class OperatorElement : Element
    {
        private char _c;
        private OperatorType _type;

        public OperatorElement(char opertaorElement)
        {
            _c = opertaorElement;
            switch(_c)
            {
                case '√':
                    _type = OperatorType.ROOT;
                    break;
                case '+':
                    _type = OperatorType.ADD;
                    break;
                case '-':
                    _type = OperatorType.SUBTRACT;
                    break;
                case '*':
                    _type = OperatorType.MULTIPLY;
                    break;
                case '/':
                    _type = OperatorType.DIVIDE;
                    break;
                case '^':
                    _type = OperatorType.EXPONENTIAL;
                    break;
                case '(':
                    _type = OperatorType.OPENBRACKET;
                    break;
                case ')':
                    _type = OperatorType.CLOSEBRACKET;
                    break;
                default:
                    break;
            }
        }

        public OperatorType Type { get => _type; private set => _type = value; }

        public override string ToString()
        {
            return _c.ToString();
        }
    }
}
