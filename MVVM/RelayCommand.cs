using System.Windows.Input;

namespace UBB_SE_2024_Popsicles.MVVM
{
    public class RelayCommand : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
<<<<<<< HEAD
            this.execute = execute;
            this.canExecute = canExecute;
=======
            execute = execute;
            canExecute = canExecute;
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
<<<<<<< HEAD
            return this.canExecute == null || this.canExecute(parameter);
=======
            return canExecute == null || canExecute(parameter);
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
        }

        public void Execute(object? parameter)
        {
<<<<<<< HEAD
            this.execute(parameter);
=======
            execute(parameter);
>>>>>>> 146baad66157a51bea86d44d5e2950cd37b116d0
        }
    }
}
