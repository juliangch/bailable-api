using bailable_api.Dtos;
using bailable_api.Models;

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
        };
        _contextDb.Add(localToCreate);
        _contextDb.SaveChanges();
        return localToCreate;
    }
}
