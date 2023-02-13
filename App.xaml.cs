using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Calculator_MVVM
{
    public partial class App : Application
    {
        private MainWindow _mainWindow;
        private CalculatorViewModel _viewModel;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            _viewModel = new CalculatorViewModel();
            _mainWindow = new MainWindow();
            _mainWindow.DataContext = _viewModel;
            _mainWindow.Show();
        }
    }
}
