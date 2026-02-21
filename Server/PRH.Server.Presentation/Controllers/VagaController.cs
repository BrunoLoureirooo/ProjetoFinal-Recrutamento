using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRH.Server.Entities.Models;
using PRH.Server.Service.Contracts;
using PRH.Shared.DataTransferObjets;
using PRH.Shared.DataTransferObjets.Vaga;
using PRH.Shared.RequestFeatures;

namespace PRH.Server.Presentation.Controllers;

[Route("api/vagas")]
[ApiController]
[Authorize]
public class VagaController : ControllerBase
{
    private readonly IServiceManager _service;
    public VagaController(IServiceManager service)
    {
        _service = service;
    }

    [HttpGet("{id:guid}", Name = "GetVagaById")]
    public async Task<IActionResult> GetVagaById(Guid id)
    {
        try
        {
            if (!await VagaCanBeAcessed(id))
                return Forbid();

            var vaga = await _service.VagaService.GetVagaById(id, trackChanges: false);
            return Ok(vaga);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("empresa/{empresaId:guid}", Name = "GetVagasByEmpresa")]
    public async Task<IActionResult> GetVagasByEmpresa(Guid empresaId, [FromQuery] VagaParameters param)
    {
        try
        {
            if (User.IsInRole("Funcionario"))
            {
                var funcEmpresa = await _service.EmpresaService.GetEmpresaByFuncionario(User, trackChanges: false);
                if (!empresaId.Equals(funcEmpresa.Id))
                    return Forbid();
            }
            else if (User.IsInRole("GestorCliente"))
            {
                var gestorEmpresa = await _service.EmpresaService.GetEmpresaByGestor(User, trackChanges: false);
                if (!empresaId.Equals(gestorEmpresa.Id))
                    return Forbid();
            }
            if (param.PageNumber is not null)
            {
                var vagasPaginated = await _service.VagaService.GetVagasByEmpresaPaginated(empresaId, param, trackChanges: false);
                return Ok(vagasPaginated);
            }

            var vagas = await _service.VagaService.GetVagasByEmpresa(empresaId, trackChanges: false);
            return Ok(vagas);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize(Roles = "Funcionario, GestorCliente")]
    [HttpGet("my", Name = "GetMyVagas")]
    public async Task<IActionResult> GetMyVagas([FromQuery] VagaParameters param)
    {
        try
        {
            EmpresaDto empresa;

            if (User.IsInRole("Funcionario"))
                empresa = await _service.EmpresaService.GetEmpresaByFuncionario(User, trackChanges: false);
            else
                empresa = await _service.EmpresaService.GetEmpresaByGestor(User, trackChanges: false);

            if (param.PageNumber is not null)
            {
                var vagasPaginated = await _service.VagaService.GetVagasByEmpresaPaginated(empresa.Id, param, trackChanges: false);
                return Ok(vagasPaginated);
            }

            var vagas = await _service.VagaService.GetVagasByEmpresa(empresa.Id, trackChanges: false);
            return Ok(vagas);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id:guid}/ficheiros-necessarios", Name = "GetRequiredFilesByVaga")]
    public async Task<IActionResult> GetRequiredFilesByVaga(Guid id)
    {
        try
        {
            if (!await VagaCanBeAcessed(id))
                return Forbid();

            var requisitos = await _service.VagaService.GetRequiredFilesByVaga(id, trackChanges: false);
            return Ok(requisitos);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id:guid}/formularios", Name = "GetFormsByVaga")]
    public async Task<IActionResult> GetFormsByVaga(Guid id)
    {
        try
        {
            if (!await VagaCanBeAcessed(id))
                return Forbid();

            var formularios = await _service.VagaService.GetFormsByVaga(id, trackChanges: false);
            return Ok(formularios);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize(Roles = "Administrador, Funcionario")]
    [HttpPost(Name = "CreateVaga")]
    public async Task<IActionResult> CreateVaga([FromBody] VagaCreateDto vaga)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        try
        {
            if (User.IsInRole("Funcionario"))
            {
                var recrutamento = await _service.RecrutamentoService.GetRecrutamentoById(vaga.RecrutamentoId.Value, trackChanges: false);
                var funcEmpresa = await _service.EmpresaService.GetEmpresaByFuncionario(User, trackChanges: false);
                if (!recrutamento.Empresa.Id.Equals(funcEmpresa.Id))
                    return Forbid();
            }
            var createdVaga = await _service.VagaService.CreateVaga(vaga, trackChanges: false);
            return CreatedAtRoute("GetVagaById", new { id = createdVaga.Id }, createdVaga);

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize(Roles = "Administrador")]
    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> UpdateVaga(Guid id, [FromBody] VagaUpdateDto vaga)
    {
        try
        {
            await _service.VagaService.UpdateVagaAsync(id, vaga, trackChanges: true);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpOptions]
    public IActionResult GetVagaOptions()
    {
        Response.Headers.Add("Allow", "GET, OPTIONS, POST, PATCH");
        return Ok();
    }

    private async Task<bool> VagaCanBeAcessed(Guid vagaId)
    {
        if (User.IsInRole("Funcionario"))
        {
            var funcEmpresa = await _service.EmpresaService.GetEmpresaByFuncionario(User, trackChanges: false);
            if (!await _service.VagaService.VagaBelongsToEmpresa(vagaId, funcEmpresa.Id, trackChanges: false))
                return false;
        }
        else if(User.IsInRole("GestorCliente"))
        {
            var gestorEmpresa = await _service.EmpresaService.GetEmpresaByGestor(User, trackChanges: false);
            if (!await _service.VagaService.VagaBelongsToEmpresa(vagaId, gestorEmpresa.Id, trackChanges: false))
                return false;
        }

        return true;
    }
}
