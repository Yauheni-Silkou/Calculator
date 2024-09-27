using System.Windows.Input;

namespace UI.Abstractions;

public class RelayCommand(Action<object> execute, Func<object, bool> canExecute = null!) : ICommand
{
    protected Action<object> execute = execute;
    protected Func<object, bool> canExecute = canExecute;

    public event EventHandler? CanExecuteChanged
    {
        add
        {
            CommandManager.RequerySuggested += value;
        }
        remove
        {
            CommandManager.RequerySuggested -= value;
        }
    }

    public bool CanExecute(object? parameter)
    {
        return canExecute == null || canExecute(parameter!);
    }

    public void Execute(object? parameter)
    {
        execute?.Invoke(parameter!);
    }
}
