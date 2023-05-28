using Microsoft.AspNetCore.Mvc;
using LifeEnsure.Services;
using LifeEnsure.Data.Models;
using LifeEnsure.Data.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace LifeEnsure.Controllers;


[ApiController]
[Route("api/[controller]")]
public class CarroController:ControllerBase{
 private readonly CarroService _carroService;
        private readonly UsuarioService _usuarioService;

        public CarroController(CarroService carroService, UsuarioService usuarioService)
        {
            _carroService = carroService;
            _usuarioService = usuarioService;
        }

        [HttpGet("getall")]
        public async Task<IEnumerable<CarroDtoOut>> GetAll()
        {
            return await _carroService.GetAll();
        }

        [HttpGet("getall/{id}")]
        public async Task<ActionResult<Carro>> GetById(int id)
        {
            var carro = await _carroService.GetById(id);
            if (carro is null)
            {
                return NotFound();
            }
            return carro;
        }

        [HttpPost("create")]
        public async Task<ActionResult<Carro>> Create(CarroDtoOut newCarroDTO)
        {
            // Validar el ID de cliente
            var usuario = await _usuarioService.GetById(newCarroDTO.IdUsuario ?? 0);
            if (usuario is null)
            {
                return BadRequest("ID de cliente no válido");
            }

            var newCarro = await _carroService.Create(newCarroDTO);
            return CreatedAtAction(nameof(GetById), new { id = newCarro.Id }, newCarro);
        }

        [HttpPut("modificar/{id}")]
        public async Task<IActionResult> Update(int id, CarroDtoOut carro)
        {
            // Validar el ID de cliente
            var usuario = await _usuarioService.GetById(carro.IdUsuario ?? 0);
            if (usuario is null)
            {
                return BadRequest("ID de cliente no válido");
            }

            await _carroService.Update(id, carro);
            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var carro = await _carroService.GetById(id);
            if (carro is null)
            {
                return NotFound();
            }

            // Validar el ID de cliente
            var usuario = await _usuarioService.GetById(carro.IdUsuario ?? 0);
            if (usuario is null)
            {
                return BadRequest("ID de cliente no válido");
            }

            await _carroService.Delete(id);
            return NoContent();
        }
}
