using Microsoft.AspNetCore.Mvc;
using PRH.Server.Service.Contracts;
using Microsoft.AspNetCore.Http;
using PRH.Shared.DataTransferObjets;
using Microsoft.AspNetCore.Authorization;
namespace PRH.Server.Presentation.Controllers;

[Route("api/templates")]
[ApiController]
[Authorize]
public class TemplateController : ControllerBase
{
    private readonly IServiceManager _service;
    private readonly string _uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "uploads/templates");

    public TemplateController(IServiceManager service)
    {
        _service = service;
        if (!Directory.Exists(_uploadDirectory))
        {
            Directory.CreateDirectory(_uploadDirectory);
        }
    }

    [HttpPost(Name = "CreateTemplate")]
    public async Task<IActionResult> CreateTemplate([FromBody] TemplateDto template)
    {
        try
        {
            var templateDto = await _service.TemplateService.CreateTemplate(template, trackChanges: false);
            return Ok(templateDto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet(Name = "GetTemplates")]
    public async Task<IActionResult> GetTemplates()
    {
        return Ok(await _service.TemplateService.GetTemplates(trackChanges: false));
    }

    [HttpGet("{id}", Name = "GetTemplateById")]
    public async Task<IActionResult> GetTemplateById(Guid id)
    {
        return Ok(await _service.TemplateService.GetTemplateById(id, trackChanges: false));
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("Nenhum arquivo enviado.");
        }

        var validExtensions = new[] { ".docx" };
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
        var fileUrl = $"{Request.Scheme}://{Request.Host}/uploads/templates/{fileName}";
        return Ok(new { FileUrl = fileUrl });
    }

    [HttpGet("empresa/{empresaId}", Name = "GetTemplatesByEmpresaId")]
    public async Task<IActionResult> GetTemplatesByEmpresaId(Guid empresaId)
    {
        try
        {
            var templates = await _service.TemplateService.GetTemplatesByEmpresaId(empresaId, trackChanges: false);
            return Ok(templates);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("delete-file")]
    public async Task<IActionResult> DeleteFile([FromQuery] string filePath, [FromQuery] Guid templateId)
    {
        if (string.IsNullOrWhiteSpace(filePath) || templateId == Guid.Empty)
            return BadRequest("Caminho inválido ou ID do template inválido");


        string adjustedPath = filePath;
        if (adjustedPath.StartsWith("/uploads", StringComparison.OrdinalIgnoreCase))
        {
            adjustedPath = adjustedPath.Substring("/uploads".Length);
        }
        adjustedPath = adjustedPath.TrimStart('/', '\\');
        adjustedPath = adjustedPath.Replace("/", Path.DirectorySeparatorChar.ToString())
                                   .Replace("\\", Path.DirectorySeparatorChar.ToString());

        var rootPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
        var fullPath = Path.Combine(rootPath, adjustedPath);

        if (System.IO.File.Exists(fullPath))
        {
            var template = await GetTemplateById(templateId);
            if (template == null)
                return NotFound($"Template com o id '{templateId}' não existe.");

            try
            {
                System.IO.File.Delete(fullPath);
                Console.WriteLine($"[DeleteFile] Arquivo '{fullPath}' deletado com sucesso.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DeleteFile] Erro durante a exclusão: {ex.Message}");
                return StatusCode(500, "Erro ao deletar arquivo.");
            }
            return Ok("Ficheiro removido com sucesso");
        }
        else
        {
            Console.WriteLine($"[DeleteFile] Arquivo não encontrado: {fullPath}");
            return NotFound($"Ficheiro não encontrado: {fullPath}");
        }
    }


    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] TemplateDto dto)
    {
        await _service.TemplateService.UpdateTemplateAsync(id, dto);
        return NoContent();
    }

    // DELETE: api/templates/{id}
    [HttpDelete("{id}", Name = "DeleteTemplate")]
    public async Task<IActionResult> DeleteTemplate(Guid id)
    {
        try
        {
            await _service.TemplateService.DeleteTemplateAsync(id);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpOptions]
    public IActionResult GetTemplateOptions()
    {
        Response.Headers["Allow"] = "GET, POST, PATCH, DELETE";
        return Ok();
    }
}
