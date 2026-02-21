using System.Collections.ObjectModel;

namespace PRH.Shared.DataTransferObjets.Campos;

public class CampoGetVagaFormsDto
{
    public CampoGetVagaFormsDto()
    {
    }

    public Guid Id { get; set; }
    public string Nome { get; set; }
    public bool IsOpcional { get; set; }
    public string Tipo { get; set; }
    public Collection<string> OpcoesCampoLookup { get; set; } = [];
}
