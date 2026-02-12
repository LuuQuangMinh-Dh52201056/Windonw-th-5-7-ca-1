using System.Windows.Input;

namespace WpfApp1.MyModels
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _action;
        private readonly Predicate<object> _canAction;
        public RelayCommand(Action<object> execute)
        {
            _action = execute;
            _canAction = null;
        }
        public RelayCommand(Action<object> action,Predicate<object> canAction)
        {
            _action = action;
            _canAction = canAction;
        }
        public virtual void Execute(object parameter)
        {
            _action(parameter);
        }
        public virtual bool CanExecute(object parameter)
        {
            return _canAction==null?true:_canAction(parameter);
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
