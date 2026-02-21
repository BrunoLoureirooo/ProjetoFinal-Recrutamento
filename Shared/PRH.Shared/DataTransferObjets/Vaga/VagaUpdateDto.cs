using PRH.Shared.DataTransferObjets.Candidatura;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;


namespace PRH.Shared.DataTransferObjets.Vaga;

public class VagaUpdateDto
{
    public VagaUpdateDto()
    {
    }
    [Required(ErrorMessage = "Vaga precisa da disponibilidade de candidatura")]
    public bool? CandidaturaDisponivel { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "Número de vagas deve ser pelo menos 1")]
    public int? NumVagas { get; set; }
    [MaxLength(512, ErrorMessage = "Descrição deve ter no máximo 512 caractéres")]
    public string? Descricao { get; set; }
    [Required(ErrorMessage = "Vaga precisa de uma modalidade")]
    public Guid? ModalidadeId { get; set; }
    [Required(ErrorMessage = "Vaga precisa de um tipo")]
    public Guid? TipoVagaId { get; set; }
    public Collection<Guid> FormularioIds { get; set; }
}
