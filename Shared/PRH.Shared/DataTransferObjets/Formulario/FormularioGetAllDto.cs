namespace PRH.Shared.DataTransferObjets.Formulario;

public class FormularioGetAllDto
{
    public FormularioGetAllDto()
    {
    }

    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string NomeUnico { get; set; }
    public LookupDto TipoFormulario { get; set; }
}
