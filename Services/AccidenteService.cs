using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LifeEnsure.Data;
using LifeEnsure.Data.Models;
using LifeEnsure.Data.DTOs;
using Microsoft.EntityFrameworkCore;

namespace LifeEnsure.Services
{
    public class AccidenteService
    {
        private readonly TigreHacksContext _context;

        public AccidenteService(TigreHacksContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AccidenteDtoOut>> GetAll()
        {
            return await _context.Accidentes
                .Include(a => a.IdUsuarioNavigation)
                .Include(a => a.IdVehiculoNavigation)
                .Select(a => new AccidenteDtoOut
                {
                    Id = a.Id,
                    Fecha = a.Fecha,
                    Hora = a.Hora,
                    TipoAccidente = a.TipoAccidente,
                    Causa = a.Causa,
                    TipoVialidad = a.TipoVialidad,
                    NombreVialidad = a.NombreVialidad,
                    Cruce = a.Cruce,
                    Sentido = a.Sentido,
                    Colonia = a.Colonia,
                    Municipio = a.Municipio,
                    Longitud = a.Longitud,
                    Latitud = a.Latitud,
                    IdVehiculo = a.IdVehiculo,
                    IdUsuario = a.IdUsuario,
                    Lesionados = a.Lesionados,
                    Muertos = a.Muertos,
                    SituacionClimatica = a.SituacionClimatica,
                    SituacionPavimento = a.SituacionPavimento,
                    Usuario = new Usuario
                    {
                        Id = a.IdUsuarioNavigation.Id,
                        Nombre = a.IdUsuarioNavigation.Nombre,
                        Telefono = a.IdUsuarioNavigation.Telefono,
                        Email = a.IdUsuarioNavigation.Email,
                        Licencia = a.IdUsuarioNavigation.Licencia,
                        Genero = a.IdUsuarioNavigation.Genero,
                        Edad = a.IdUsuarioNavigation.Edad
                    },
                    Vehiculo = new CarroDtoOut
                    {
                        Id = a.IdVehiculoNavigation.Id,
                        Placas = a.IdVehiculoNavigation.Placas,
                        Modelo = a.IdVehiculoNavigation.Modelo,
                        ValorDeducible = a.IdVehiculoNavigation.ValorDeducible,
                        IdUsuario = a.IdVehiculoNavigation.IdUsuario
                    }
                })
                .ToListAsync();
        }

        public async Task<Accidente?> GetById(int id)
        {
            return await _context.Accidentes
                .Include(a => a.IdUsuarioNavigation)
                .Include(a => a.IdVehiculoNavigation)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Accidente> Create(AccidenteDtoIn newAccidenteDTO)
        {
            var newAccidente = new Accidente
            {
                Fecha = newAccidenteDTO.Fecha,
                Hora = newAccidenteDTO.Hora,
                TipoAccidente = newAccidenteDTO.TipoAccidente,
                Causa = newAccidenteDTO.Causa,
                TipoVialidad = newAccidenteDTO.TipoVialidad,
                NombreVialidad = newAccidenteDTO.NombreVialidad,
                Cruce = newAccidenteDTO.Cruce,
                Sentido = newAccidenteDTO.Sentido,
                Colonia = newAccidenteDTO.Colonia,
                Municipio = newAccidenteDTO.Municipio,
                Longitud = newAccidenteDTO.Longitud,
                Latitud = newAccidenteDTO.Latitud,
                IdVehiculo = newAccidenteDTO.IdVehiculo,
                IdUsuario = newAccidenteDTO.IdUsuario,
                Lesionados = newAccidenteDTO.Lesionados,
                Muertos = newAccidenteDTO.Muertos,
                SituacionClimatica = newAccidenteDTO.SituacionClimatica,
                SituacionPavimento = newAccidenteDTO.SituacionPavimento
            };

            _context.Accidentes.Add(newAccidente);
            await _context.SaveChangesAsync();

            return newAccidente;
        }

        public async Task Update(int id, AccidenteDtoIn accidente)
        {
            var existingAccidente = await GetById(id);

            if (existingAccidente is not null)
            {
                existingAccidente.Fecha = accidente.Fecha;
                existingAccidente.Hora = accidente.Hora;
                existingAccidente.TipoAccidente = accidente.TipoAccidente;
                existingAccidente.Causa = accidente.Causa;
                existingAccidente.TipoVialidad = accidente.TipoVialidad;
                existingAccidente.NombreVialidad = accidente.NombreVialidad;
                existingAccidente.Cruce = accidente.Cruce;
                existingAccidente.Sentido = accidente.Sentido;
                existingAccidente.Colonia = accidente.Colonia;
                existingAccidente.Municipio = accidente.Municipio;
                existingAccidente.Longitud = accidente.Longitud;
                existingAccidente.Latitud = accidente.Latitud;
                existingAccidente.IdVehiculo = accidente.IdVehiculo;
                existingAccidente.IdUsuario = accidente.IdUsuario;
                existingAccidente.Lesionados = accidente.Lesionados;
                existingAccidente.Muertos = accidente.Muertos;
                existingAccidente.SituacionClimatica = accidente.SituacionClimatica;
                existingAccidente.SituacionPavimento = accidente.SituacionPavimento;

                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var accidenteToDelete = await GetById(id);

            if (accidenteToDelete is not null)
            {
                _context.Accidentes.Remove(accidenteToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
