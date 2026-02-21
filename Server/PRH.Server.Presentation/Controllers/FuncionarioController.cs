using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRH.Server.Service.Contracts;
using PRH.Shared.RequestFeatures;

namespace PRH.Server.Presentation.Controllers;

[Route("api/Funcionarios")]
[ApiController]
[Authorize]
public class FuncionarioController : ControllerBase
{
    private readonly IServiceManager _service;
    public FuncionarioController(IServiceManager service)
    {
        _service = service;
    }

    [Authorize(Roles = "Administrador")]
    [HttpGet(Name = "GetFuncionarios")]
    public async Task<IActionResult> GetFuncionarios([FromQuery] int? page = null, [FromQuery] int? pageSize = null)
    {
        if (page is not null)
            return Ok(await _service.FuncionarioService.GetFuncionariosPaginated((int)page, pageSize is null ? 10 : (int)pageSize, trackChanges: false));

        return Ok(await _service.FuncionarioService.GetFuncionarios(trackChanges: false));
    }

    [Authorize(Roles = "Administrador, Funcionario")]
    [HttpGet("empresa/{empresaId:guid}", Name = "GetFuncionariosByEmpresa")]
    public async Task<IActionResult> GetFuncionariosByEmpresa(Guid empresaId)
    {
        try
        {
            if (User.IsInRole("Funcionario"))
            {
                var funcEmpresa = await _service.EmpresaService.GetEmpresaByFuncionario(User, trackChanges: false);
                if (!empresaId.Equals(funcEmpresa.Id))
                    return Forbid();
            }

            var funcs = await _service.FuncionarioService.GetFuncionariosByEmpresa(empresaId, trackChanges: false);
            return Ok(funcs);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize(Roles = "Administrador, Funcionario")]
    [HttpGet("{id}", Name = "GetFuncionarioById")]
    public async Task<IActionResult> GetFuncionarioById(string id)
    {
        try
        {
            var funcionario = await _service.FuncionarioService.GetFuncionarioById(id, trackChanges: false);
            return Ok(funcionario);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }



    [HttpOptions]
    public IActionResult GetFuncionarioOptions()
    {
        Response.Headers.Add("Allow", "GET, OPTIONS, POST");
        return Ok();
    }
}

