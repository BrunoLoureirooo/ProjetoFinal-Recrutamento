using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRH.Server.Service.Contracts;
using PRH.Shared.RequestFeatures;

namespace PRH.Server.Presentation.Controllers;

[Route("api/admissoes")]
[ApiController]
[Authorize]
public class AdmissaoController : ControllerBase
{
    private readonly IServiceManager _service;

    public AdmissaoController(IServiceManager service)
    {
        _service = service;
    }

    [Authorize(Roles = "Administrador, Funcionario")]
    [HttpGet(Name = "GetAdmissoes")]
    public async Task<IActionResult> GetAdmissoes([FromQuery] AdmissaoParameters param)
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
                var admissoesPaginadas = await _service.CandidaturaService.GetAdmissaoPaginated(param, trackChanges: false);
                return Ok(admissoesPaginadas);
            }

            var admissoes = await _service.CandidaturaService.GetAdmissao(param, trackChanges: false);
            return Ok(admissoes);
        }
        catch (Exception e)
        {
            return BadRequest(new { message = "Erro ao buscar admissões.", details = e.Message });
        }
    }


    [HttpOptions]
    public IActionResult GetAdmissoesOptions()
    {
        Response.Headers.Add("Allow", "GET, OPTIONS");
        return Ok();
    }
}
