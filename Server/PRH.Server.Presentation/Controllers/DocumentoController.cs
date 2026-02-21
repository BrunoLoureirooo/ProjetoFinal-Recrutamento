using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRH.Server.Service.Contracts;
using PRH.Shared.DataTransferObjets;
using System.Net.Mime;

namespace PRH.Server.Presentation.Controllers
{
    [Route("api/admissoes/documentos")]
    [ApiController]
    [Authorize]
    public class DocumentoController : ControllerBase
    {
        private readonly IDocumentoService _documentoService;
        private readonly string _uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "uploads/documentos");

        public DocumentoController(IServiceManager serviceManager)
        {
            _documentoService = serviceManager.DocumentoService;
        }

        [HttpPost("template")]
        public async Task<IActionResult> CreateFromTemplate(
    [FromQuery] Guid candidaturaId,
    [FromQuery] Guid templateId,
    [FromBody] Dictionary<string, string>? extras)
        {
            try
            {
                var dto = await _documentoService
                                 .CreateDocumentoFromTemplateAsync(candidaturaId, templateId, extras);
                return Ok(dto);
            }
            catch (MissingTemplateValuesException mex)
            {
                // devolve 400 + lista de placeholders
                return BadRequest(mex.CamposFaltantes);
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetByCandidatura([FromQuery] Guid candidaturaId)
        {
            var docs = await _documentoService.GetDocumentosByCandidaturaAsync(candidaturaId);
            return Ok(docs);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var doc = await _documentoService.GetDocumentoByIdAsync(id);
            return Ok(doc);
        }


        [HttpPost("createdocumento")]
        public async Task<IActionResult> CreateDocumento([FromBody] DocumentoDto documento)
        {
            try
            {
                var documentoDto = await _documentoService.CreateDocumentoAsync(documento, trackChanges: false);
                return Ok(documentoDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id:guid}/bytes")]
        public async Task<IActionResult> GetDocumentoBytesAsync([FromRoute] Guid id)
        {
            var bytes = await _documentoService.ObterBytesDocumentoAsync(id);

            if (bytes == null || bytes.Length == 0)
                return NotFound(new { Mensagem = $"Documento com id {id} não encontrado ou sem conteúdo." });

            const string contentType = MediaTypeNames.Application.Pdf;

            var fileName = $"documento-{id}.pdf";

            return File(bytes, contentType, fileName);
        }


        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Nenhum arquivo enviado.");
            }

            var validExtensions = new[] { ".pdf" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            if (!Array.Exists(validExtensions, ext => ext == fileExtension))
            {
                return BadRequest("Tipo de arquivo não permitido.");
            }

            var fileName = Path.GetRandomFileName() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(_uploadDirectory, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            // Construindo a URL completa utilizando o Request:
            var fileUrl = $"{Request.Scheme}://{Request.Host}/uploads/documentos/{fileName}";
            return Ok(new { FileUrl = fileUrl });
        }

        [HttpPost("assinatura")]
        public async Task<IActionResult> ReceiveSignatures([FromBody] AssinaturaDto assinatura)
        {
            if (assinatura.documentoId == Guid.Empty)
            {
                return BadRequest("O ID do documento é obrigatório.");
            }
            if (assinatura.Assinaturas == null || assinatura.Assinaturas.Count != 2)
            {
                return BadRequest("São esperadas 2 assinaturas.");
            }

            try
            {
                await _documentoService.AssinarDocumentoAsync(assinatura, trackChanges: false);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] DocumentoDto dto)
        {
            await _documentoService.UpdateDocumentoAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteDocumento(Guid id)
        {
            try
            {
                await _documentoService.DeleteDocumentoAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpOptions]
        public IActionResult GetDocumentoOptions()
        {
            Response.Headers["Allow"] = "GET, POST, PATCH, DELETE";
            return Ok();
        }
    }
}
