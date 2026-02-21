namespace PRH.Shared.DataTransferObjets
{
    public class DocumentoDto
    {
        public DocumentoDto()
        {
            Caminho = string.Empty;
        }

        public Guid Id { get; set; }
        public Guid? TemplateId { get; set; }
        public string Caminho { get; set; }
        public Guid CandidaturaId { get; set; }
        public Guid TipoDocumentoId { get; set; }
        public bool DocumentoAssinado { get; set; }
    }
}