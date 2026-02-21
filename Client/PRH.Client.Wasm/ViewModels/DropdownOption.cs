namespace PRH.Client.Wasm.ViewModels;

public class DropdownOption<T>
{
    public string Label { get; set; }
    public T Value { get; set; }

    public DropdownOption(string label, T value)
    {
        Label = label;
        Value = value;
    }
}
