using Microsoft.AspNetCore.Mvc;
using LifeEnsure.Services;
using LifeEnsure.Data.Models;
using LifeEnsure.Data.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace LifeEnsure.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController:ControllerBase{
    private readonly UsuarioService _service;
    public UsuarioController(UsuarioService service)
    {
        _service = service;
    }
    [HttpGet("getall")]
    public async Task<IEnumerable<Usuario>> Get()
    {
        return await _service.GetAll();
    }
    [HttpGet("getall/{id}")]
    public async Task<ActionResult<Usuario>> GetById(int id)
    {
        var client = await _service.GetById(id);

        if (client is null)
        {
            return NotFound(new { message = $"El cliente con ID={id} no existe" });
        }
        return client;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(Usuario usuario)
    {
        var newUsuario = await _service.Create(usuario);


        return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, newUsuario); //revuelve status 201
    }

    [HttpPut("update/{id}")]

    public async Task<IActionResult> Update(int id, Usuario usuario)
    {
        if (id != usuario.Id)
            return BadRequest(new { message = $"El ID={id} de la URL no coincide con el ID del cuerpo({usuario.Id})." });

        var clientToUpdate = await _service.GetById(id);
        if (clientToUpdate is not null)
        {
            await _service.Update(id, usuario);
            return NoContent();
        }
        else
        {
             return NotFound(new { message = $"El cliente con ID={id} no existe" });

        }
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userToDelete = await _service.GetById(id);
        if (userToDelete is not null)
        {
            await _service.Delete(id);
            return Ok();
        }
        else
        {
            return NotFound(new { message = $"El cliente con ID={id} no existe" });

        }


    }

    
}
