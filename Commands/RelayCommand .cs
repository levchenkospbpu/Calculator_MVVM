using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Calculator_MVVM
{
    class RelayCommand : ICommand
    {
        private readonly Action execute;
        private readonly Action<object> executeWithParameter;
        private readonly Func<bool> canExecute;
        private readonly List<EventHandler> canExecuteSubscribers = new List<EventHandler>();

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/> class.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        public RelayCommand(Action execute)
          : this(execute, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/> class.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            this.execute = execute;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/> class.
        /// Initializes command with parameterized delegate.
        /// </summary>
        /// <param name="execute">action delegate.</param>
        public RelayCommand(Action<object> execute)
          : this(execute, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/> class.
        /// Initializes command with parameterized delegate and predicate.
        /// </summary>
        /// <param name="execute">action delegate.</param>
        /// <param name="canExecute">predicate to check action possibility.</param>
        public RelayCommand(Action<object> execute, Func<bool> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            this.executeWithParameter = execute;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            // wire the CanExecutedChanged event only if the canExecute func
            // is defined (that improves perf when canExecute is not used)
            add
            {
                if (this.canExecute != null)
                {
                    CommandManager.RequerySuggested += value;
                    this.canExecuteSubscribers.Add(value);
                }
            }

            remove
            {
                if (this.canExecute != null)
                {
                    CommandManager.RequerySuggested -= value;
                    this.canExecuteSubscribers.Remove(value);
                }
            }
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public void Execute(object parameter)
        {
            if (parameter == null)
            {
                // If execute is null, make sure the signature of the execute method
                // does not expect a parameter. This will lead to execute being null.
                this.execute();
            }
            else
            {
                this.executeWithParameter(parameter);
            }
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        /// <returns>
        /// true if this command can be executed; otherwise, false.
        /// </returns>
        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute();
        }

        /// <summary>
        /// Ask the UI to reevaluate CanExecute.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
