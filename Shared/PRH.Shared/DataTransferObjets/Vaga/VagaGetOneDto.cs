using PRH.Shared.DataTransferObjets.Candidatura;
using System.Collections.ObjectModel;
using PRH.Shared.DataTransferObjets.Formulario;
using PRH.Shared.DataTransferObjets.Cargo;


namespace PRH.Shared.DataTransferObjets.Vaga;

public class VagaGetOneDto
{
    public VagaGetOneDto()
    {
    }
    public Guid Id { get; set; }
    public Guid RecrutamentoId { get; set; }
    public bool CandidaturaDisponivel { get; set; }
    public int NumVagas { get; set; }
    public string Descricao { get; set; }
    public LookupDto Modalidade { get; set; }
    public CargoGetAllDto Cargo { get; set; }
    public LookupDto TipoVaga { get; set; }
    public Collection<CandidaturaGetOneVagaDto> Candidaturas { get; set; }
    public Collection<FormularioGetAllDto> Formularios { get; set; }
}
