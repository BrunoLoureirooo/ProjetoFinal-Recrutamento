using PRH.Shared.DataTransferObjets.Candidatura;
using PRH.Shared.DataTransferObjets.Cargo;
using System.Collections.ObjectModel;


namespace PRH.Shared.DataTransferObjets.Vaga;

public class VagaGetOneRecrutamentoDto
{
    public VagaGetOneRecrutamentoDto()
    {
    }
    public Guid Id { get; set; }
    public bool CandidaturaDisponivel { get; set; }
    public int NumVagas { get; set; }
    public string Descricao { get; set; }
    public LookupDto Modalidade { get; set; }
    public CargoGetAllDto Cargo { get; set; }
    public LookupDto TipoVaga { get; set; }
}
