using bailable_api.Database;
using bailable_api.Dtos;
using bailable_api.Models;

namespace bailable_api.Service;


public interface ILocalService
{
    public bool CreateLocal(RegisterLocalRequestDto registerLocalRequestDto);
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
}
