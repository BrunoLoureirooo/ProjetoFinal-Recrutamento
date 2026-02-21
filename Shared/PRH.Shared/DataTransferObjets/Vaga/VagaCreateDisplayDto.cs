using PRH.Shared.DataTransferObjets.Cargo;
using System.ComponentModel.DataAnnotations;

namespace PRH.Shared.DataTransferObjets.Vaga;
public class VagaCreateDisplayDto
{
    public VagaCreateDisplayDto()
    {
    }

    [Required(ErrorMessage = "Vaga tem de estar ou não disponivel para candidatura")]
    public bool CandidaturaDisponivel { get; set; }

    [Required(ErrorMessage = "Vaga precisa de numero de vagas")]
    public int NumVagas { get; set; }

    [Required(ErrorMessage = "Vaga precisa de uma descrição")]
    public string Descricao { get; set; }

    [Required(ErrorMessage = "Vaga precisa de uma modalidade")]
    public LookupDto Modalidade { get; set; }

    [Required(ErrorMessage = "Vaga precisa de um cargo")]
    public CargoGetAllDto Cargo { get; set; }

    [Required(ErrorMessage = "Vaga precisa de um tipo")]
    public LookupDto TipoVaga { get; set; }

}

