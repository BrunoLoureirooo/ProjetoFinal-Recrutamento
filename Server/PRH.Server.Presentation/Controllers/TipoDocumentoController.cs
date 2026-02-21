using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRH.Server.Service.Contracts;
using PRH.Shared.DataTransferObjets;

namespace PRH.Server.Presentation.Controllers
{
    [Route("api/tipoDocumentos")]
    [ApiController]
    [Authorize]
    public class TipoDocumentoController : ControllerBase
    {
        private readonly ITipoDocumentoService _tipoDocumentoService;

        public TipoDocumentoController(IServiceManager serviceManager)
        {
            _tipoDocumentoService = serviceManager.TipoDocumentoService;
        }

        // POST: api/tipodocumentos
        [HttpPost(Name = "CreateTipoDocumento")]
        public async Task<IActionResult> CreateTipoDocumento([FromBody] TipoDocumentoDto dto)
        {
            if (dto == null)
                return BadRequest("Dados do TipoDocumento não podem ser nulos.");

            try
            {
                var created = await _tipoDocumentoService.CreateTipoDocumentoAsync(dto, trackChanges: false);
                return CreatedAtRoute("GetTipoDocumentoById", new { id = created.Id }, created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/tipodocumentos
        [HttpGet(Name = "GetTipoDocumentos")]
        public async Task<IActionResult> GetTipoDocumentos()
        {
            var list = await _tipoDocumentoService.GetTipoDocumentosAsync(trackChanges: false);
            return Ok(list);
        }

        // GET: api/tipodocumentos/{id}
        [HttpGet("{id}", Name = "GetTipoDocumentoById")]
        public async Task<IActionResult> GetTipoDocumentoById(Guid id)
        {
            try
            {
                var dto = await _tipoDocumentoService.GetTipoDocumentoByIdAsync(id, trackChanges: false);
                return Ok(dto);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // Patch: api/tipodocumentos/{id}
        [HttpPatch("{id}", Name = "UpdateTipoDocumento")]
        public async Task<IActionResult> UpdateTipoDocumento(Guid id, [FromBody] TipoDocumentoDto dto)
        {

            await _tipoDocumentoService.UpdateTipoDocumentoAsync(id, dto);
            return NoContent();

        }

        // DELETE: api/tipodocumentos/{id}
        [HttpDelete("{id}", Name = "DeleteTipoDocumento")]
        public async Task<IActionResult> DeleteTipoDocumento(Guid id)
        {
            try
            {
                await _tipoDocumentoService.DeleteTipoDocumentoAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpOptions]
        public IActionResult GetTipoDocumentoOptions()
        {
            Response.Headers["Allow"] = "GET, POST, PATCH, DELETE";
            return Ok();
        }
    }
}
