using LifeEnsure.Data;
using LifeEnsure.Data.Models;
using LifeEnsure.Data.DTOs;
using Microsoft.EntityFrameworkCore;
namespace LifeEnsure.Services;

public class LoginService
{
    private readonly TigreHacksContext _context;
    public LoginService(TigreHacksContext context)
    {
        _context = context;
    }
    public async Task <Usuario?> GetClient(UsuarioDto client){
        //nos traemos tipo administrador cumpla las condiciones o un null
        return await _context.Usuarios.SingleOrDefaultAsync(
            x=>x.Email==client.Email && x.Nombre==client.Nombre);
    }
}