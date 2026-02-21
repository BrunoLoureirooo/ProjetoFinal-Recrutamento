using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRH.Server.Service.Contracts;
using PRH.Shared.DataTransferObjets;

namespace PRH.Server.Presentation.Controllers;

[Route("api/empresas")]
[ApiController]
[Authorize]
public class EmpresaController : ControllerBase
{
    private readonly IServiceManager _service;
    public EmpresaController(IServiceManager service)
    {
        _service = service;
    }



    [HttpGet(Name = "GetEmpresas")]
    public async Task<IActionResult> GetEmpresas()
    {
        return Ok(await _service.EmpresaService.GetEmpresas(trackChanges: false));
    }

    [Authorize(Roles = "Funcionario, GestorCliente")]
    [HttpGet("my", Name = "GetMyEmpresa")]
    public async Task<IActionResult> GetMyEmpresa()
    {
        try
        {
            EmpresaDto empresa;
            if (User.IsInRole("Funcionario"))
                empresa = await _service.EmpresaService.GetEmpresaByFuncionario(User, trackChanges: false);
            else
                empresa = await _service.EmpresaService.GetEmpresaByGestor(User, trackChanges: false);

            return Ok(empresa);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpOptions]
    public IActionResult GetEmpresaOptions()
    {
        Response.Headers.Add("Allow", "GET, OPTIONS");
        return Ok();
    }
}
