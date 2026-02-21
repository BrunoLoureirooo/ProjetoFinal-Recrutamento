using PRH.Shared.DataTransferObjets.Vaga;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace PRH.Shared.DataTransferObjets.Recrutamento;

public class RecrutamentoCreateDto
{
    public RecrutamentoCreateDto()
    {
    }

    [Required(ErrorMessage = "Recrutamento precisa de um nome")]
    [MaxLength(128, ErrorMessage = "Nome deve ter no máximo 128 caractéres")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "Recrutamento precisa de uma empresa")]
    public Guid? EmpresaId { get; set; }

    [Required(ErrorMessage = "Recrutamento precisa de vagas")]
    [MinLength(1, ErrorMessage = "Recrutamento precisa de pelo menos 1 vaga")]
    public Collection<VagaCreateRecrutamentoDto> Vagas { get; set; }
}
