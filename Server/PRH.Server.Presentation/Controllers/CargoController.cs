using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRH.Server.Entities.Models;
using PRH.Server.Service.Contracts;
using PRH.Shared.DataTransferObjets.Cargo;
using PRH.Shared.DataTransferObjets.Recrutamento;
using PRH.Shared.RequestFeatures;

namespace PRH.Server.Presentation.Controllers;

[Route("api/cargos")]
[ApiController]
[Authorize]
public class CargoController : ControllerBase
{
    private readonly IServiceManager _service;
    public CargoController(IServiceManager service)
    {
        _service = service;
    }

    [HttpGet(Name = "GetCargos")]
    public async Task<IActionResult> GetCargos([FromQuery] CargoParameters param)
    {
        if (param.PageNumber is not null)
        {
            var cargosPaginated = await _service.CargoService.GetCargosPaginated(param, trackChanges: false);
            return Ok(cargosPaginated);
        }

        var cargos = await _service.CargoService.GetCargos(trackChanges: false);
        return Ok(cargos);
    }

    [HttpGet("{id}", Name = "GetCargoById")]
    public async Task<IActionResult> GetCargoById(Guid id)
    {
        try
        {
            var cargo = await _service.CargoService.GetCargoByIdAsync(id, trackChanges: false);
            return Ok(cargo);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }
    [Authorize(Roles = "Administrador, Funcionario")]
    [HttpPost(Name = "CreateCargo")]
    public async Task<IActionResult> CreateCargo([FromBody] CargoCreateDto cargo)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        try
        {
            var createdCargo = await _service.CargoService.CreateCargo(cargo, trackChanges: false);
            return CreatedAtRoute("GetCargoById", new { id = createdCargo }, createdCargo);

        }
        catch (Exception e)
        {
            return BadRequest(e.InnerException.Message);
        }
    }

    [HttpOptions]
    public IActionResult GetCargoOptions()
    {
        Response.Headers.Add("Allow", "GET, POST, OPTIONS");
        return Ok();
    }
}
