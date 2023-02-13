using System.Globalization;

namespace Calculator_MVVM
{
    public class NumberElement : Element
    {
        private double _number;

        public NumberElement(string number)
        {
            // Try parsing in the current culture
            if (!double.TryParse(number, NumberStyles.Any, CultureInfo.CurrentCulture, out _number) &&
                // Then try in US english
                !double.TryParse(number, NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out _number) &&
                // Then in neutral language
                !double.TryParse(number, NumberStyles.Any, CultureInfo.InvariantCulture, out _number))
            {
                _number = double.MaxValue; ;
            }
        }

        public double Number { get => _number; private set => _number = value; }

        public override string ToString()
        {
            return (_number).ToString();
        }
    }
}
