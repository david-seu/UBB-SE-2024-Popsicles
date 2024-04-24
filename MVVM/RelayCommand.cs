using System.Windows.Input;

namespace UBB_SE_2024_Popsicles.MVVM
{
    public class RelayCommand : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? relayCommandParameter)
        {
            return this.canExecute == null || this.canExecute(relayCommandParameter);
        }

        public void Execute(object? relayCommandParameter)
        {
            this.execute(relayCommandParameter);
        }
    }
}
