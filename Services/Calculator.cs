namespace Services;

public sealed class Calculator
{
    public const string ZERO = "0";

    private static readonly int _storageCapacity = 20;
    private readonly Queue<string> _storage = new(_storageCapacity);

    public string Entry { get; set; } = ZERO;

    public string Output { get; private set; } = string.Empty;

    public string ClearButtonLabel => ClearStatus switch
    {
        Common.ClearStatus.NoneCleared => "C",
        _ => "AC"
    };

    public string[] Storage => [.. _storage];

    public ClearStatus ClearStatus { get; private set; }

    public void ChangeEntry(string parameter)
    {
        if (Entry == ZERO)
        {
            if (OperatorsManager.IsOperatorBinary(parameter) || parameter == ".")
            {
                Entry += parameter;
            }
            else
            {
                Entry = parameter;
            }
        }
        else
        {
            Entry = $"{Entry}{parameter}";
        }

        ClearStatus = ClearStatus.NoneCleared;
    }

    public void DeleteEntry()
    {
        if (Entry != ZERO)
        {
            Entry = $"{Entry[..^1]}";
            ClearStatus = ClearStatus.NoneCleared;

            if (string.IsNullOrWhiteSpace(Entry))
            {
                ClearEntry();
            }
        }
    }


    public void Calculate()
    {
        var result = ExpressionEvaluator.Evaluate(Entry, out string formatted);
        Output = $"{formatted}= {result}";
        ClearEntry();
        if (_storage.Count > _storageCapacity)
        {
            _storage.Dequeue();
        }

        _storage.Enqueue(Output);
        ClearStatus = ClearStatus.EntryCleared;
    }

    public void AllClear()
    {
        ClearEntry();
        _storage.Clear();
        Output = string.Empty;
        ClearStatus = ClearStatus.AllCleared;
    }

    public void ClearEntry()
    {
        Entry = ZERO;
        ClearStatus = ClearStatus.EntryCleared;
    }
}
