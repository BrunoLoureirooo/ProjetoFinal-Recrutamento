using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PRH.Server.Service.Contracts;
using PRH.Shared.DataTransferObjets.Recrutamento;
using PRH.Shared.RequestFeatures;
using System.Security.Claims;

namespace PRH.Server.Presentation.Controllers;

[Route("api/recrutamentos")]
[ApiController]
[Authorize]
public class RecrutamentoController : ControllerBase
{
    private readonly IServiceManager _service;
    public RecrutamentoController(IServiceManager service)
    {
        _service = service;
    }

    [Authorize(Roles = "Administrador, Funcionario")]
    [HttpGet(Name = "GetRecrutamentos")]
    public async Task<IActionResult> GetRecrutamentos([FromQuery] RecrutamentoParameters param)
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
                var recrutamentosPaginated = await _service.RecrutamentoService.GetRecrutamentosPaginated(param, trackChanges: false);
                return Ok(recrutamentosPaginated);
            }

            var recrutamentos = await _service.RecrutamentoService.GetRecrutamentos(param, trackChanges: false);
            return Ok(recrutamentos);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }

    [Authorize(Roles = "Administrador, Funcionario")]
    [HttpGet("{id:guid}", Name = "GetRecrutamentoById")]
    public async Task<IActionResult> GetRecrutamentoById(Guid id)
    {
        try
        {
            var recrutamento = await _service.RecrutamentoService.GetRecrutamentoById(id, trackChanges: false);

            if (User.IsInRole("Funcionario"))
            {
                var funcEmpresa = await _service.EmpresaService.GetEmpresaByFuncionario(User, trackChanges: false);
                if (!recrutamento.Empresa.Id.Equals(funcEmpresa.Id))
                    return Forbid();
            }
            
            return Ok(recrutamento);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize(Roles = "Administrador, Funcionario")]
    [HttpPost(Name = "CreateRecrutamento")]
    public async Task<IActionResult> CreateRecrutamento([FromBody] RecrutamentoCreateDto recrutamento)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        try
        {
            if (User.IsInRole("Funcionario"))
            {
                var funcEmpresa = await _service.EmpresaService.GetEmpresaByFuncionario(User, trackChanges: false);
                if (!recrutamento.EmpresaId.Equals(funcEmpresa.Id))
                    return Forbid();
            }
            var createdRecrutamento = await _service.RecrutamentoService.CreateRecrutamento(recrutamento, trackChanges: false);
            return CreatedAtRoute("GetRecrutamentoById", new { id = createdRecrutamento.Id }, createdRecrutamento);

        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize(Roles = "Administrador, Funcionario")]
    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> UpdateRecrutamento(Guid id, [FromBody] RecrutamentoUpdateDto recrutamento)
    {
        try
        {
            if (User.IsInRole("Funcionario"))
            {
                var funcEmpresa = await _service.EmpresaService.GetEmpresaByFuncionario(User, trackChanges: false);
                var r = await _service.RecrutamentoService.GetRecrutamentoById(id, trackChanges: false);
                if (!r.Empresa.Id.Equals(funcEmpresa.Id))
                    return Forbid();

                recrutamento.EmpresaId = funcEmpresa.Id;
            }
            await _service.RecrutamentoService.UpdateRecrutamento(id, recrutamento, trackChanges: true);
            return NoContent();
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpOptions]
    public IActionResult GetRecrutamentoOptions()
    {
        Response.Headers.Add("Allow", "GET, OPTIONS, POST, PATCH");
        return Ok();
    }
}

