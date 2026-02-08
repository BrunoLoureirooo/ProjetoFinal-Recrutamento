using System;
using Microsoft.AspNetCore.Mvc;
using PRH.Server.Service.Contracts;

namespace PRH.Server.Presentation.Controllers;

[Route("api/empresas")]
[ApiController]
public class EmpresaController : ControllerBase
{
    private readonly IServiceManager _service;
    public EmpresaController(IServiceManager service) 
    {
        _service = service;
    }

    [HttpGet(Name = "GetEmpresa")]
    public async Task<IActionResult> GetEmpresa()
    {
        return Ok(await _service.EmpresaService.GetEmpresas(trackChanges: false));
    }
}
