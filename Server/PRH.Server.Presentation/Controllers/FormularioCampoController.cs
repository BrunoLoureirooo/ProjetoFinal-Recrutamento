using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRH.Server.Entities.Models;
using PRH.Server.Service.Contracts;
using PRH.Shared.DataTransferObjets;

namespace PRH.Server.Presentation.Controllers
{
    [Route("api/FormularioCampos")]
    [ApiController]
    [Authorize]
    public class FormularioCampoController : ControllerBase
    {
        private readonly IServiceManager _service;

        public FormularioCampoController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet("{id}", Name = "GetFormularioCampoById")]
        public async Task<IActionResult> GetFormularioCampoById(Guid id)
        {
            try
            {
                var formularioCampo = await _service.FormularioCampoService.GetFormularioCampoByIdAsync(id, trackChanges: false);

                if (formularioCampo == null)
                    return NotFound($"FormularioCampo com o id '{id}' não foi encontrado.");

                return Ok(formularioCampo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter o campo: {ex.Message}");
            }
        }

        [HttpPost(Name = "CreateFormularioCampo")]
        public async Task<IActionResult> CreateFormularioCampo([FromBody] FormularioCampoDto formularioCampo)
        {
            try
            {
                var createdFormularioCampo = await _service.FormularioCampoService.CreateFormularioCampo(formularioCampo, trackChanges: false);
                return Ok(createdFormularioCampo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // Novo endpoint para atualizar o campo "Ativo"
        [HttpPut("Toggle/{id}")]
        public async Task<IActionResult> UpdateFormularioCampoAtivo(Guid id)
        {
            try
            {
                var updatedFormularioCampo = await _service.FormularioCampoService.AlterarStatusAtivo(id, true);

                if (updatedFormularioCampo == null)
                    return NotFound($"FormularioCampo com o id '{id}' não foi encontrado.");

                return Ok(updatedFormularioCampo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar o campo: {ex.Message}");
            }
        }


        [HttpOptions]
        public IActionResult GetFormularioOptions()
        {
            Response.Headers.Add("Allow", "GET, OPTIONS, POST, DELETE");
            return Ok();
        }
    }
}

