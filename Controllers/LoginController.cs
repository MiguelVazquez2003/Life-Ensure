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
        public async Task<ActionResult> Login(UsuarioDto usuarioDto)
        {
            var usuario = await loginService.GetClient(usuarioDto);
            if (usuario is null)
            {
                return BadRequest(new { message = $"Credenciales invalidas" });
            }
            string jwtToken = GenerateToken(usuario);

            return Ok(new { token = jwtToken });
        }
        private string GenerateToken(Usuario usuario)
        {
            var claims = new[]{
            new Claim(ClaimTypes.Name,usuario.Nombre),
            new Claim(ClaimTypes.Email,usuario.Email),
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString())
        };



            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:ClientKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var securityToken = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: creds);

            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }
    

    
}