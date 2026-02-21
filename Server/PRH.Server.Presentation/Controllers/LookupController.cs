using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRH.Server.Service.Contracts;

namespace PRH.Server.Presentation.Controllers;

[Route("api/lookups")]
[ApiController]
[Authorize]
public class LookupController : ControllerBase
{
    private readonly IServiceManager _service;
    public LookupController(IServiceManager service)
    {
        _service = service;
    }


    [Route("modalidades")]
    [HttpGet(Name = "GetModalidades")]
    public async Task<IActionResult> GetModalidades()
    {
        return Ok(await _service.ModalidadeService.GetModalidades(trackChanges: false));
    }

    [Route("tipos-vaga")]
    [HttpGet(Name = "GetTiposVaga")]
    public async Task<IActionResult> GetTiposVaga()
    {
        return Ok(await _service.TipoVagaService.GetTiposVaga(trackChanges: false));
    }

    [Route("tipos-campo")]
    [HttpGet(Name = "GetTiposCampo")]
    public async Task<IActionResult> GetTiposCampo()
    {
        return Ok(await _service.TipoCampoService.GetTiposCampo(trackChanges: false));
    }

    [HttpGet("tipos-campo/{id:guid}", Name = "GetTipoCampoById")]
    public async Task<IActionResult> GetTipoCampoById(Guid id)
    {
        try
        {
            var tipoCampo = await _service.TipoCampoService.GetTipoCampoById(id, trackChanges: false);
            return Ok(tipoCampo);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("tipos-formulario")]
    [HttpGet(Name = "GetTiposFormulario")]
    public async Task<IActionResult> GetTiposFormulario()
    {
        return Ok(await _service.TipoFormularioService.GetTiposFormulario(trackChanges: false));
    }
    [Route("estados")]
    [HttpGet(Name = "GetEstados")]
    public async Task<IActionResult> GetEstados()
    {
        return Ok(await _service.EstadoService.GetEstados(trackChanges: false));
    }

    [Route("resultados")]
    [HttpGet(Name = "GetResultados")]
    public async Task<IActionResult> GetResultados()
    {
        return Ok(await _service.ResultadoService.GetResultados(trackChanges: false));
    }


    /*[HttpGet(Name = "GetLookups")]
    public async Task<IActionResult> GetLookups()
    {
        return Ok(await _service.LookupService.GetLookups(trackChanges: false));
    }

    [HttpGet("{Tipo}", Name = "GetLookupByTipo")]
    public async Task<IActionResult> GetLookupByTipo(string Tipo)
    {
        var recrutamento = await _service.LookupService.GetLookupByTipo(Tipo, trackChanges: false);

        if (recrutamento == null) return BadRequest("Nao existem Lookups do tipo \'" + Tipo );

        return Ok(recrutamento);
    }*/

    /*[HttpPost(Name = "CreateLookup")]
    public async Task<IActionResult> CreateLookup([FromBody] LookupComVagasDto recrutamento)
    {
        try
        {
            var recrutamentoDto = _service.LookupService.CreateLookup(recrutamento, trackChanges: false);
            return Ok(recrutamentoDto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }*/

    [HttpOptions]
    public IActionResult GetLookupOptions()
    {
        Response.Headers.Add("Allow", "GET, OPTIONS");
        return Ok();
    }
}

