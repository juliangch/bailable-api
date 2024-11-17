using bailable_api.Dtos;
using bailable_api.Models;
using Microsoft.EntityFrameworkCore;

namespace bailable_api.Database;

public class LocalDao
{
    private readonly ContextDb _contextDb;

    public LocalDao(ContextDb contextDb)
    {
        _contextDb = contextDb;
    }

    public Local CreateLocal(RegisterLocalRequestDto registerLocalRequestDto)
    {
        Local localToCreate = new Local()
        {
            Nombre = registerLocalRequestDto.Nombre,
            Capacidad = registerLocalRequestDto.Capacidad,
            Direccion = registerLocalRequestDto.Direccion,
            Zona = registerLocalRequestDto.Zona,
            Duenio = registerLocalRequestDto.Duenio
        };
        _contextDb.Add(localToCreate);
        _contextDb.SaveChanges();
        return localToCreate;
    }
    public Local GetLocalById(Guid id)
    {
        Local localEncontrado = _contextDb.Locales.FirstOrDefault(l => l.LocalId == id);
        return localEncontrado;
    }
    public Local GetLocalByIdWEventos(Guid id)
    {
        Local localEncontrado = _contextDb.Locales.Include(l => l.Eventos).FirstOrDefault(l => l.LocalId == id);
        return localEncontrado;
    }

    public Local DeleteLocal(Local local)
    {
        _contextDb.Locales.Remove(local);
        _contextDb.SaveChanges();
        return local;
    }
}
