namespace PRH.Client.Wasm.Services.FilterState;

public class EntrevistaFilterStateService
{
    public string? NomeCandidato { get; set; }
    public DateTime? After { get; set; }
    public DateTime? Before { get; set; }
    public Guid? Resultado { get; set; }
    public bool InListMode { get; set; } = true;
    public DateTime? CurrentMonthCalendar { get; set; }
}
