using LifeEnsure.Data;
using LifeEnsure.Data.Models;
using LifeEnsure.Data.DTOs;
using Microsoft.EntityFrameworkCore;


public class CarroService{
    
private readonly TigreHacksContext _context;

public CarroService(TigreHacksContext context){
    _context=context;
}

public async Task<IEnumerable<CarroDtoOut>> GetAll()
{
    return await _context.Carros
        .Include(a => a.IdUsuarioNavigation) // Incluye la entidad Usuario relacionada
        .Select(a => new CarroDtoOut
        {
            Id = a.Id,
            Placas = a.Placas,
            Modelo = a.Modelo,
            ValorDeducible = a.ValorDeducible,
            IdUsuario = a.IdUsuario,
           

        })
        .ToListAsync();
}

    public async Task<Carro?> GetById(int id)
    {
        return  await _context.Carros.FindAsync(id);
    }

    public async Task<Carro> Create(CarroDtoOut newCarroDTO)
    {
        var newCarro = new Carro();

        newCarro.Placas=newCarroDTO.Placas;
        newCarro.Modelo=newCarroDTO.Modelo;
        newCarro.ValorDeducible=newCarroDTO.ValorDeducible;
        newCarro.IdUsuario=newCarroDTO.IdUsuario;

        _context.Carros.Add(newCarro);
        await _context.SaveChangesAsync();

        return newCarro;
    }



    public async Task Update(int id, CarroDtoOut carro)
    {


        var existingcarro = await GetById(id);


        if (existingcarro is not null)
        {
            existingcarro.Placas=carro.Placas;
            existingcarro.Modelo=carro.Modelo;
            existingcarro.ValorDeducible=carro.ValorDeducible;
            existingcarro.IdUsuario=carro.IdUsuario;

            await _context.SaveChangesAsync();
        }


    }

    public async Task Delete(int id)
    {
        var carroToDelete = await GetById(id);

        if (carroToDelete is not null)
        {
            _context.Carros.Remove(carroToDelete);
            await _context.SaveChangesAsync();
        }
    }





}

