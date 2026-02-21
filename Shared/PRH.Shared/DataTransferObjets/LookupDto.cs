using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRH.Shared.DataTransferObjets;

public class LookupDto
{
    public LookupDto()
    {
    }
    public Guid Id { get; set; }
    public string Descricao { get; set; }
}
