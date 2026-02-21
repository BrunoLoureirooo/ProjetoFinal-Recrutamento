using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRH.Server.Service.Contracts;
using PRH.Shared.DataTransferObjets;
using PRH.Shared.DataTransferObjets.Candidatura;
using PRH.Shared.RequestFeatures;

namespace PRH.Server.Presentation.Controllers;

[Route("api/candidaturas")]
[ApiController]
[Authorize]
public class CandidaturaController : ControllerBase
{
    private readonly IServiceManager _service;
    public CandidaturaController(IServiceManager service)
    {
        _service = service;
    }

    [Authorize(Roles = "Administrador, Funcionario")]
    [HttpGet(Name = "GetCandidaturas")]
    public async Task<IActionResult> GetCandidaturas([FromQuery] CandidaturaParameters param)
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
                var candidaturasPaginated = await _service.CandidaturaService.GetCandidaturasPaginated(param, trackChanges: false);
                return Ok(candidaturasPaginated);
            }

            var candidaturas = await _service.CandidaturaService.GetCandidaturas(param, trackChanges: false);
            return Ok(candidaturas);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }

    [Authorize(Roles = "Administrador, Funcionario, GestorCliente")]
    [HttpGet("empresa/{empresaId:guid}", Name = "GetCandidaturasByEmpresa")]
    public async Task<IActionResult> GetCandidaturasByEmpresa(Guid empresaId, [FromQuery] CandidaturaParameters param)
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
                var candidaturasPaginated = await _service.CandidaturaService.GetCandidaturasByEmpresaPaginated(empresaId, param, trackChanges: false);
                return Ok(candidaturasPaginated);
            }

            var candidaturas = await _service.CandidaturaService.GetCandidaturasByEmpresa(empresaId, param, trackChanges: false);
            return Ok(candidaturas);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }

    [Authorize(Roles = "Funcionario, GestorCliente")]
    [HttpGet("my", Name = "GetMyCandidaturas")]
    public async Task<IActionResult> GetMyCandidaturas([FromQuery] CandidaturaParameters param)
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
                var candidaturasPaginated = await _service.CandidaturaService.GetCandidaturasByEmpresaPaginated(empresa.Id, param, trackChanges: false);
                return Ok(candidaturasPaginated);
            }

            var candidaturas = await _service.CandidaturaService.GetCandidaturasByEmpresa(empresa.Id, param, trackChanges: false);
            return Ok(candidaturas);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize(Roles = "Administrador, Funcionario, GestorCliente")]
    [HttpGet("{id:guid}", Name = "GetCandidaturaById")]
    public async Task<IActionResult> GetCandidaturaById(Guid id)
    {
        try
        {
            if (!await CandidaturaCanBeAcessed(id))
                return Forbid();

            var candidatura = await _service.CandidaturaService.GetCandidaturaById(id, trackChanges: false);

            return Ok(candidatura);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize(Roles = "Administrador, Funcionario, GestorCliente")]
    [HttpPost(Name = "CreateCandidatura")]
    public async Task<IActionResult> CreateCandidatura([FromBody] CandidaturaCreateDto candidatura)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        try
        {
            if (User.IsInRole("Funcionario"))
            {
                var funcEmpresa = await _service.EmpresaService.GetEmpresaByFuncionario(User, trackChanges: false);
                if (!await _service.VagaService.VagaBelongsToEmpresa((Guid)candidatura.VagaId, funcEmpresa.Id, trackChanges: false)) ;
                    return Forbid();
            }
            else if (User.IsInRole("GestorCliente"))
            {
                var gestorEmpresa = await _service.EmpresaService.GetEmpresaByGestor(User, trackChanges: false);
                if (!await _service.VagaService.VagaBelongsToEmpresa((Guid)candidatura.VagaId, gestorEmpresa.Id, trackChanges: false))
                    return Forbid();
            }
            var createdCandidatura = await _service.CandidaturaService.CreateCandidatura(candidatura, trackChanges: false);
            return CreatedAtRoute("GetCandidaturaById", new { id = createdCandidatura.Id }, createdCandidatura);
        }
        catch(ApplicationException e)
        {
            return StatusCode(500, e.Message);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message + e.InnerException?.Message);
        }
    }

    [Authorize(Roles = "Administrador, Funcionario")]
    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> UpdateCandidatura(Guid id, [FromBody] CandidaturaUpdateDto candidatura)
    {
        try
        {
            if (!await CandidaturaCanBeAcessed(id))
                return Forbid();
            
            await _service.CandidaturaService.UpdateCandidaturaAsync(id, candidatura, trackChanges: true);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
   	}
    }

    [HttpOptions]
    public IActionResult GetCandidaturaOptions()
    {
        Response.Headers.Add("Allow", "GET, OPTIONS, POST, PATCH");
        return Ok();
    }

    private async Task<bool> CandidaturaCanBeAcessed(Guid candidaturaId)
    {
        if (User.IsInRole("Funcionario"))
        {
            var candidaturaBd = await _service.CandidaturaService.GetCandidaturaById(candidaturaId, trackChanges: false);
            var funcEmpresa = await _service.EmpresaService.GetEmpresaByFuncionario(User, trackChanges: false);
            if (!candidaturaBd.Empresa.Id.Equals(funcEmpresa.Id))
                return false;
        }
        else if (User.IsInRole("GestorCliente"))
        {
            var candidaturaBd = await _service.CandidaturaService.GetCandidaturaById(candidaturaId, trackChanges: false);
            var gestorEmpresa = await _service.EmpresaService.GetEmpresaByGestor(User, trackChanges: false);
            if (!candidaturaBd.Empresa.Id.Equals(gestorEmpresa.Id))
                return false;
        }

        return true;
    }
}

