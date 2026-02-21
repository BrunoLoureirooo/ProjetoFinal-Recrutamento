using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
﻿using PRH.Shared.DataTransferObjets.Candidatura;
using PRH.Shared.DataTransferObjets.Formulario;

namespace PRH.Shared.DataTransferObjets.Vaga;

public class VagaCreateRecrutamentoDto
{
    public VagaCreateRecrutamentoDto()
    {
    }

    [Required(ErrorMessage = "Vaga tem de estar ou não disponivel para candidatura")]
    public bool CandidaturaDisponivel { get; set; }

    [Required(ErrorMessage = "Vaga precisa de numero de vagas")]
    [Range(1, int.MaxValue, ErrorMessage = "Número de vagas deve ser pelo menos 1")]
    public int NumVagas { get; set; }

    [Required(ErrorMessage = "Vaga precisa de uma descrição")]
    [MaxLength(512, ErrorMessage = "Descrição deve ter no máximo 512 caractéres")]
    public string Descricao { get; set; }

    [Required(ErrorMessage = "Vaga precisa de uma modalidade")]
    public Guid? ModalidadeId { get; set; }

    [Required(ErrorMessage = "Vaga precisa de um cargo")]
    public Guid? CargoId { get; set; }

    [Required(ErrorMessage = "Vaga precisa de um tipo")]
    public Guid? TipoVagaId { get; set; }

    [Required(ErrorMessage = "Vaga precisa de formulários")]
    [MinLength(1, ErrorMessage = "Vaga precisa de pelo menos 1 formulário")]
    public Collection<Guid> FormularioIds { get; set; }
}
