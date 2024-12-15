using bailable_api.Database;
using bailable_api.Dtos;
using bailable_api.Models;

namespace bailable_api.Service;


public interface ILocalService
{
    public bool CreateLocal(RegisterLocalRequestDto registerLocalRequestDto);
    public DeleteLocalResponseDto DeleteLocal(Guid id);
    public Local EditLocalByOwner(EditLocalRequestDto editLocalRequestDto);
    public void CheckLocalOwnership(Guid localId, Guid ownerId);
    public List<Local> GetLocalesByDuenioId(Guid duenioId);
}
public class LocalService : ILocalService
{
    private readonly LocalDao _localDao;
    private readonly UserDao _userDao;
    public LocalService(ContextDb contextDb)
    {
        _localDao = new LocalDao(contextDb);
        _userDao = new UserDao(contextDb);
    }


    public bool CreateLocal(RegisterLocalRequestDto registerLocalRequestDto)
    {
        bool response = false;
        User duenio = _userDao.GetUserById(registerLocalRequestDto.DuenioId);
        if (duenio != null && duenio.Role == Constants.UserRoleEnum.DUENIO)
        {
            registerLocalRequestDto.Duenio = duenio;
            Local localCreated = _localDao.CreateLocal(registerLocalRequestDto);
            if (localCreated != null)
            {
                response = true;
            }
        }
        return response;
    }
    public DeleteLocalResponseDto DeleteLocal(Guid id)
    {
        Local localEncontrado = _localDao.GetLocalById(id);
        if (localEncontrado == null)
        {
            return new DeleteLocalResponseDto()
            {
                Success = false,
                Message = "Local no encontrado"
            };
        }
        if (localEncontrado.Eventos != null && localEncontrado.Eventos.Any())
        {
            return new DeleteLocalResponseDto()
            {
                Success = false,
                Message = "El local tiene eventos proximos."
            };
        }
        Local localBorrado = _localDao.DeleteLocal(localEncontrado);
        if (localBorrado != null)
        {
            return new DeleteLocalResponseDto()
            {
                Success = true,
                Message = "Local borrado con exito."
            };
        }
        else
        {
            return new DeleteLocalResponseDto()
            {
                Success = false,
                Message = "Error al borrar el local."
            };
        }
    }

    public void CheckLocalOwnership(Guid localId, Guid userId)
    {
        if (!_localDao.IsLocalOwnedBy(localId, userId))
        {
            throw new InvalidOperationException("El usuario no puede editar el local");
        }
    }

    public Local EditLocalByOwner(EditLocalRequestDto editLocalReqDto)
    {
        var userDuenio = _userDao.GetUserById(editLocalReqDto.UserId);

        Local editedLocal = new Local()
        {
            Capacidad = editLocalReqDto.Local.Capacidad,
            Direccion = editLocalReqDto.Local.Direccion,
            Nombre = editLocalReqDto.Local.Nombre,
            Zona = editLocalReqDto.Local.Zona,
            Duenio = userDuenio,
        };

        return _localDao.UpdateLocal(editedLocal) > 0 ? editedLocal : throw new Exception("No se pudo actualizar el local");
    }
    public List<Local> GetLocalesByDuenioId(Guid duenioId)
    {
        return _localDao.GetLocalesByDuenio(duenioId);
    }
}

