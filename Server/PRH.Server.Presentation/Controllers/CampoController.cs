using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRH.Server.Entities.Models;
using PRH.Server.Service.Contracts;
using PRH.Shared.DataTransferObjets.Campo;

namespace PRH.Server.Presentation.Controllers;

[Route("api/Campos")]
[ApiController]
[Authorize]
public class CampoController : ControllerBase
{
    private readonly IServiceManager _service;
    public CampoController(IServiceManager service)
    {
        _service = service;
    }

    [HttpGet(Name = "GetCampos")]
    public async Task<IActionResult> GetCampos()
    {
        return Ok(await _service.CampoService.GetCampos(trackChanges: false));
    }

    [Authorize(Roles = "Administrador")]
    [HttpPost(Name = "CreateCampo")]
    public async Task<IActionResult> CreateCampo([FromBody] CampoCreateDto campo)
    {
        try
        {
            var CampoGetDto = await _service.CampoService.CreateCampoAsync(campo, trackChanges: false);
            return Ok(CampoGetDto);
        }
        catch (Exception e)
        {
            var inner = e.InnerException?.Message;
            var message = inner != null ? $"Erro ao criar campo: {inner}" : $"Erro ao criar campo: {e.Message}";
            return BadRequest(message);
        }
    }

    [HttpOptions]
    public IActionResult GetCampoOptions()
    {
        Response.Headers.Add("Allow", "GET, OPTIONS, POST");
        return Ok();
    }
}

