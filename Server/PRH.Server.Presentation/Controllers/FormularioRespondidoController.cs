using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRH.Server.Entities.Models;
using PRH.Server.Service.Contracts;
using PRH.Shared.DataTransferObjets;

namespace PRH.Server.Presentation.Controllers;

[Route("api/formularios-respondidos")]
[ApiController]
[Authorize]
public class FormularioRespondidoController : ControllerBase
{
	private readonly IServiceManager _service;
	public FormularioRespondidoController(IServiceManager service)
	{
		_service = service;
	}

    [Authorize(Roles = "Administrador, Funcionario, GestorCliente")]
    [HttpGet("candidatura/{candidaturaId}",Name = "GetRespostasByCandidatura")]
    public async Task<IActionResult> GetRespostasByCandidatura(Guid candidaturaId)
    {
        try
        {
            if (User.IsInRole("Funcionario"))
            {
                var funcEmpresa = await _service.EmpresaService.GetEmpresaByFuncionario(User, trackChanges: false);
                var candidatura = await _service.CandidaturaService.GetCandidaturaById(candidaturaId, trackChanges: false);

                if (!candidatura.Empresa.Id.Equals(funcEmpresa.Id))
                    return Forbid();
            }
            else if (User.IsInRole("GestorCliente"))
            {
                var gestorEmpresa = await _service.EmpresaService.GetEmpresaByGestor(User, trackChanges: false);
                var candidatura = await _service.CandidaturaService.GetCandidaturaById(candidaturaId, trackChanges: false);

                if (!candidatura.Empresa.Id.Equals(gestorEmpresa.Id))
                    return Forbid();
            }
            var respostas = await _service.FormularioRespondidoService.GetRespostasByCandidatura(candidaturaId, trackChanges: false);
            return Ok(respostas);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpOptions]
    public IActionResult GetFormularioRespondidoOptions()
    {
        Response.Headers.Add("Allow", "GET, OPTIONS");
        return Ok();
    }
}

