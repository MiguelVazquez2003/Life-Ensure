using Microsoft.AspNetCore.Mvc;
using LifeEnsure.Services;
using LifeEnsure.Data.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using LifeEnsure.Data.DTOs;
namespace LifeEnsure.Controllers;


[ApiController]
[Route("api/loginclient")]
public class LoginClientController : ControllerBase
{

    private readonly LoginService loginService;
    private IConfiguration config; //me permite trabajar con el archivo app settings .json
    public LoginClientController(LoginService service, IConfiguration config)
    {
        this.loginService = service;
        this.config = config;
    }

    [HttpPost("authenticate")]
    public async Task<ActionResult> Login(UsuarioDto clientDto)
    {
        var client = await loginService.GetClient(clientDto);
        if (client is null)
        {
            return BadRequest(new { message = $"Credenciales invalidas" });
        }
       

        return Ok(new {message=$"Ingreso con exito"});
    }

    

    
}