using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRH.Server.Entities.Models;
using PRH.Server.Service.Contracts;
using PRH.Shared.DataTransferObjets.Entrevista;
using PRH.Shared.DataTransferObjets.Vaga;
using PRH.Shared.RequestFeatures;

namespace PRH.Server.Presentation.Controllers;

[Route("api/entrevistas")]
[ApiController]
[Authorize]
public class EntrevistaController : ControllerBase
{
    private readonly IServiceManager _service;
    public EntrevistaController(IServiceManager service)
    {
        _service = service;
    }

    [Authorize(Roles = "Administrador, Funcionario")]
    [HttpGet(Name = "GetEntrevistas")]
    public async Task<IActionResult> GetEntrevistas([FromQuery] EntrevistaParameters param)
    {
        try
        {
            if (User.IsInRole("Funcionario"))
            {
                var funcEmpresa = await _service.EmpresaService.GetEmpresaByFuncionario(User, trackChanges: false);
                param.EmpresaId = funcEmpresa.Id;
            }

            if (param.PageNumber is not null)
            {
                var entrevistasPaginated = await _service.EntrevistaService.GetEntrevistasPaginated(param, trackChanges: false);
                return Ok(entrevistasPaginated);
            }

            var entrevistas = await _service.EntrevistaService.GetEntrevistas(param, trackChanges: false);
            return Ok(entrevistas);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }

    [Authorize(Roles = "Administrador, Funcionario")]
    [HttpGet("{id:guid}", Name = "GetEntrevistaById")]
    public async Task<IActionResult> GetEntrevistaById(Guid id)
    {
        try
        {
            if (User.IsInRole("Funcionario"))
            {
                var funcEmpresa = await _service.EmpresaService.GetEmpresaByFuncionario(User, trackChanges: false);
                Guid entrevistaEmpresaId = await _service.EntrevistaService.GetEmpresaOfEntrevistaById(id, trackChanges: false);
                if (!entrevistaEmpresaId.Equals(funcEmpresa.Id))
                    return Forbid();
            }

            var entrevista = await _service.EntrevistaService.GetEntrevistaById(id, trackChanges: false);
            return Ok(entrevista);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize(Roles = "Administrador, Funcionario")]
    [HttpPost(Name = "CreateEntrevista")]
    public async Task<IActionResult> CreateEntrevista([FromBody] EntrevistaCreateDto entrevista)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        try
        {
            if (User.IsInRole("Funcionario"))
            {
                var candidatura = await _service.CandidaturaService.GetCandidaturaById(entrevista.CandidaturaId, trackChanges: false);
                var funcEmpresa = await _service.EmpresaService.GetEmpresaByFuncionario(User, trackChanges: false);
                if (!candidatura.Empresa.Id.Equals(funcEmpresa.Id))
                    return Forbid();
            }
            var createdEntrevista = await _service.EntrevistaService.CreateEntrevista(entrevista, trackChanges: false);
            return CreatedAtRoute("GetEntrevistaById", new { id = createdEntrevista.Id }, createdEntrevista  );
        }
        catch (ApplicationException e)
        {
            return StatusCode(500, e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.InnerException.Message);
        }
    }

    [Authorize(Roles = "Administrador, Funcionario")]
    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> UpdateEntrevista(Guid id, [FromBody] EntrevistaUpdateDto entrevista)
    {
        try
        {
            if (User.IsInRole("Funcionario"))
            {
                var funcEmpresa = await _service.EmpresaService.GetEmpresaByFuncionario(User, trackChanges: false);
                Guid entrevistaEmpresaId = await _service.EntrevistaService.GetEmpresaOfEntrevistaById(id, trackChanges: false);
                if (!entrevistaEmpresaId.Equals(funcEmpresa.Id))
                    return Forbid();
            }
            await _service.EntrevistaService.UpdateEntrevistaAsync(id, entrevista, false);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.InnerException?.Message ?? e.Message);
        }
    }

    [HttpOptions]
    public IActionResult GetRecrutamentoOptions()
    {
        Response.Headers.Add("Allow", "GET, POST, PATCH, OPTIONS");
        return Ok();
    }
}
