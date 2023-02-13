using System;
using System.Windows.Input;

namespace Calculator_MVVM
{
    public class CalculatorViewModel : NotifyPropertyBase<CalculatorViewModel>
    {
        private CalculatorModel _model;
        private string _content = string.Empty;
        private string _result = string.Empty;

        public CalculatorViewModel()
        {
            _model = new CalculatorModel();
            OnSolveButtonClickCommand = new RelayCommand(OnSolveButtonClick);
        }

        public string Content { get => _content; set => _content = value; }
        public string Result { get => _result; set => _result = value; }

        public ICommand OnSolveButtonClickCommand { get; private set; }

        private void OnSolveButtonClick()
        {
            Tuple<string, double> result = _model.Calculate(Content);
            Result = result.Item1 + " = " + result.Item2;
            NotifyPropertyChanged(x => x.Result);
        }
    }
}
