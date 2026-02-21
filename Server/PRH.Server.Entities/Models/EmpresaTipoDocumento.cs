namespace PRH.Server.Entities.Models
{
    public class EmpresaTipoDocumento
    {
        public Guid EmpresaId { get; set; }
        public virtual Empresa? Empresa { get; set; }

        public Guid TipoDocumentoId { get; set; }
        public virtual TipoDocumento? TipoDocumento { get; set; }
    }
}