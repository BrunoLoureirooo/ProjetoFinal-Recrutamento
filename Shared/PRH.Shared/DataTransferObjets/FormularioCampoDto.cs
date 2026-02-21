using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using PRH.Shared.DataTransferObjets.Campo;

namespace PRH.Shared.DataTransferObjets;

public class FormularioCampoDto
{
    public FormularioCampoDto()
    {
    }
    public Guid Id { get; set; }
    public Guid FormularioId { get; set; }
    public Guid CampoId { get; set; }
    public CampoGetDto Campo { get; set; }
    public bool Ativo {  get; set; }
}

