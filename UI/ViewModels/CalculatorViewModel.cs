using UI.Abstractions;

namespace UI.ViewModels;

public sealed class CalculatorViewModel : ViewModelBase
{
    private readonly Services.Calculator calculator = new();

    private int _caretIndex;

    private RelayCommand? _buttonClickCommand;
    private RelayCommand? _calculateButtonClickCommand;
    private RelayCommand? _clearButtonClickCommand;
    private RelayCommand? _deleteButtonClickCommand;

    public int CaretIndex
    {
        get { return _caretIndex; }
        set { _caretIndex = value; }
    }

    public string Entry
    {
        get => calculator.Entry;
        set
        {
            if (calculator.Entry != value)
            {
                calculator.Entry = value;
                OnPropertyChanged();
            }
        }
    }

    public string Output => calculator.Output;

    public string ClearButtonLabel => calculator.ClearButtonLabel;

    public RelayCommand ButtonClickCommand => _buttonClickCommand ??= new(
        execute: (parameter) =>
        {
            var c = CaretIndex;
            calculator.ChangeEntry(parameter?.ToString() ?? throw new Exception("There is no parameter for a button."));
            CaretIndex = c;

            OnPropertyChanged(nameof(Entry));
            OnPropertyChanged(nameof(ClearButtonLabel));
        });

    public RelayCommand CalculateButtonClickCommand => _calculateButtonClickCommand ??= new(
        execute: (parameter) =>
        {
            calculator.Calculate();

            OnPropertyChanged(nameof(Entry));
            OnPropertyChanged(nameof(Output));
            OnPropertyChanged(nameof(ClearButtonLabel));
        });

    public RelayCommand ClearButtonClickCommand => _clearButtonClickCommand ??= new(
        execute: (_) =>
        {
            if (Entry == Services.Calculator.ZERO)
            {
                calculator.AllClear();
            }
            else
            {
                calculator.ClearEntry();
            }

            OnPropertyChanged(nameof(Entry));
            OnPropertyChanged(nameof(Output));
            OnPropertyChanged(nameof(ClearButtonLabel));
        });

    public RelayCommand DeleteButtonClickCommand => _deleteButtonClickCommand ??= new(
        execute: (_) =>
        {
            calculator.DeleteEntry();
            OnPropertyChanged(nameof(Entry));
            OnPropertyChanged(nameof(ClearButtonLabel));
        });
}
