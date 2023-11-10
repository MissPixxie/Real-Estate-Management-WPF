using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Modern_Real_Estate.Utilities
{
    public class RelayCommand : ICommand
    {
        // Privat medlemsvariabel med namnet execute som är av typen Action<object>.
        // Den används för att lagra en åtgärd (en metod) som kan utföras när kommandot körs.
        private Action<object> execute;
        // Privat medlemsvariabel med namnet canExecute som är av typen Func<object, bool>.
        // Den används för att lagra en funktion som avgör om kommandot kan utföras (kan exekveras) i en given kontext.
        private Func<object, bool> canExecute;

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        // Detta är en konstruktor för RelayCommand-klassen. Den tar två parametrar: execute (en åtgärd som ska utföras när kommandot körs)
        // och canExecute (en funktion som avgör om kommandot kan köras). canExecute-parametern är valfri och kan vara null.
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            execute(parameter);
        }
    }
}
