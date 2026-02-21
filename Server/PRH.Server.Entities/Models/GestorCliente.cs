namespace PRH.Server.Entities.Models;

public class GestorCliente : BaseModel
{        
    public Guid EmpresaId { get; set; }
    public virtual Empresa Empresa { get; set; }

    public string ApplicationUserId { get; set; }
    public virtual ApplicationUser ApplicationUser { get; set; }
}
