using PRH.Shared.DataTransferObjets.Vaga;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRH.Shared.DataTransferObjets.Recrutamento;

public class RecrutamentoAfterCreateDto
{
    public RecrutamentoAfterCreateDto()
    {
    }

    public Guid Id { get; set; }
    public string Nome { get; set; }
    public DateTime Data { get; set; }
    public Guid EmpresaId { get; set; }
}
