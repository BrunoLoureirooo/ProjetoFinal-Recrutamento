using PRH.Shared.DataTransferObjets.Vaga;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRH.Shared.DataTransferObjets.Recrutamento;

public class RecrutamentoGetOneDto
{
    public RecrutamentoGetOneDto()
    {
    }

    public Guid Id { get; set; }
    public string Nome { get; set; }
    public DateTime Data { get; set; }
    public EmpresaDto Empresa { get; set; }
    public Collection<VagaGetOneRecrutamentoDto> Vagas { get; set; }
    public int TotalVagas { get; set; }
    public Collection<string> Cargos { get; set; }
}
