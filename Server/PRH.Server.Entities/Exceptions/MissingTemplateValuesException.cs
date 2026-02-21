public class MissingTemplateValuesException : Exception
{
    public IEnumerable<string> CamposFaltantes { get; }
    public MissingTemplateValuesException(IEnumerable<string> campos)
        : base("Faltam valores para placeholders.") =>
        CamposFaltantes = campos;
}
