using System.Collections.Generic;
using System.Text;

namespace Calculator_MVVM
{
    public class Parser
    {
        private List<Element> _elements = new List<Element>();

        public List<Element> Parse(string s)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                if (char.IsDigit(c) || c == '.')
                {
                    sb.Append(c);
                }
                if (i + 1 < s.Length)
                {
                    char d = s[i + 1];
                    if (char.IsDigit(d) == false && d != '.' && sb.Length > 0)
                    {
                        _elements.Add(new NumberElement(sb.ToString()));
                        sb.Remove(0, sb.Length);
                    }
                }

                if (c == '+' || c == '-' || c == '*' || c == '/' || c == '^' || c == '(' || c == ')' || c == '√')
                {
                    _elements.Add(new OperatorElement(c));
                }
            }
            if (sb.Length > 0)
            {
                _elements.Add(new NumberElement(sb.ToString()));
            }
            return _elements;
        }
    }
}
