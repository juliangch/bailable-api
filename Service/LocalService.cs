using bailable_api.Database;
using bailable_api.Dtos;
using bailable_api.Models;

namespace bailable_api.Service;


public interface ILocalService
{
    public bool CreateLocal(RegisterLocalRequestDto registerLocalRequestDto);
    public DeleteLocalRequestDto DeleteLocal(Guid id);
}
public class LocalService : ILocalService
{
    private readonly LocalDao _localDao;
    public LocalService(ContextDb contextDb)
    {
        _localDao = new LocalDao(contextDb);
    }


    public bool CreateLocal(RegisterLocalRequestDto registerLocalRequestDto)
    {
        bool response = false;
        Local localCreated = _localDao.CreateLocal(registerLocalRequestDto);
        if (localCreated != null)
        {
            response = true;
        }
        return response;
    }
    public DeleteLocalRequestDto DeleteLocal(Guid id)
    {
        Local localEncontrado = _localDao.GetLocalById(id);
        if (localEncontrado == null)
        {
            return new DeleteLocalRequestDto()
            {
                Success = false,
                Error = "Local no encontrado"
            };
        }
        if (localEncontrado.Eventos != null && localEncontrado.Eventos.Any())
        {
            return new DeleteLocalRequestDto()
            {
                Success = false,
                Error = "El local tiene eventos proximos."
            };
        }
        Local localBorrado = _localDao.DeleteLocal(localEncontrado);
        if (localBorrado != null)
        {
            return new DeleteLocalRequestDto()
            {
                Success = true,
                Error = "Local borrado con exito."
            };
        }
        else
        {
            return new DeleteLocalRequestDto()
            {
                Success = false,
                Error = "Error al borrar el local."
            };
        }
    }
}

