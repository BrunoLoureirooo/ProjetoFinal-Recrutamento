using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRH.Server.Service.Contracts;
using PRH.Shared.DataTransferObjets;
using PRH.Shared.DataTransferObjets.Formulario;

namespace PRH.Server.Presentation.Controllers;

[Route("api/formularios")]
[ApiController]
[Authorize]
public class FormularioController : ControllerBase
{
	private readonly IServiceManager _service;
	public FormularioController(IServiceManager service)
	{
		_service = service;
	}

    [HttpGet(Name = "GetFormularios")]
    public async Task<IActionResult> GetFormularios([FromQuery] int? page = null, [FromQuery] int? pageSize = null)
    {
        if (page is not null)
            return Ok(await _service.FormularioService.GetFormulariosPaginated((int)page, pageSize is null ? 10 : (int)pageSize, trackChanges: false));

        return Ok(await _service.FormularioService.GetFormularios(trackChanges: false));
    }

    [HttpGet("{id}", Name = "GetFormularioById")]
	public async Task<IActionResult> GetFormularioById(Guid id)
	{
		try
		{
			var formulario = await _service.FormularioService.GetFormularioById(id, trackChanges: false);
            return Ok(formulario);
        }
		catch(Exception e)
		{
            return BadRequest(e.Message);
        }
	}

    [Authorize(Roles = "Administrador")]
    [HttpPatch("{id}", Name = "UpdateFormulario")]
    public async Task<IActionResult> UpdateFormulario(Guid id, [FromBody] FormularioUpdateDto dto)
    {
        if (dto == null)
            return BadRequest("Objeto de atualização está nulo.");

        try
        {
            var updatedFormulario = await _service.FormularioService.UpdateFormularioAsync(id, dto, trackChanges: true);
            return Ok(updatedFormulario);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception e)
        {
            var errorMessage = e.InnerException?.Message ?? e.Message;
            return StatusCode(500, $"Erro interno ao atualizar formulário: {errorMessage}");
        }
    }

    [Authorize(Roles = "Administrador")]
    [HttpPost(Name = "CreateFormulario")]
    public async Task<IActionResult> CreateFormulario([FromBody] FormularioCreateDto Formulario)
    {
            try
            {
                var FormularioGetAllDto = await _service.FormularioService.CreateFormulario(Formulario, trackChanges: false);
                return Ok(FormularioGetAllDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
    }

    [HttpOptions]
    public IActionResult GetFormularioOptions()
    {
        Response.Headers.Add("Allow", "GET, OPTIONS, POST");
        return Ok();
    }
}

