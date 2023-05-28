using Microsoft.EntityFrameworkCore;
using LifeEnsure.Data;
using LifeEnsure.Data.Models;
namespace LifeEnsure.Services;

public class UsuarioService{

    private readonly TigreHacksContext _context;

    public UsuarioService(TigreHacksContext context)
    {
        _context = context;
    }

     public async Task<IEnumerable<Usuario>> GetAll(){
        return await _context.Usuarios.ToListAsync();
    
    }
    public async Task<Usuario?> GetById(int id){
        return await _context.Usuarios.FindAsync(id);
            }

    public async Task<Usuario> Create(Usuario newUser){
        _context.Usuarios.Add(newUser);
        await _context.SaveChangesAsync();

        return  newUser;

    }
    public async Task Update (int id,Usuario user){
        var existingUser= await GetById(id);
        if(existingUser is not null){
            existingUser.Nombre=user.Nombre;
            existingUser.Telefono=user.Telefono;
            existingUser.Email=user.Email;
            existingUser.Licencia=user.Licencia;
            existingUser.Genero=user.Genero;
            existingUser.Edad=user.Edad;
           await  _context.SaveChangesAsync();
        }
    }

    public async Task Delete (int id){
        var userToDelete= await GetById(id);

        if(userToDelete is not null){
            _context.Usuarios.Remove(userToDelete);
            await _context.SaveChangesAsync();
        }
}
}