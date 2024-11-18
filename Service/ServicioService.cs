using bailable_api.Database;
using bailable_api.Dtos;
using bailable_api.Models;

namespace bailable_api.Service;


public interface IServicioService
{
    public ServiciosByEventoResponseDto getServiciosByEvento(Guid eventoId);
}
public class ServicioService : IServicioService
{
    private readonly ServicioDao _servicioDao;
    private readonly EventoDao _eventoDao;
    public ServicioService(ContextDb context)
    {
        _servicioDao = new ServicioDao(context);
        _eventoDao = new EventoDao(context);
    }
    public ServiciosByEventoResponseDto getServiciosByEvento(Guid eventoId)
    {
        EventoWCapacidadDto evento = _eventoDao.GetEventoById(eventoId);
        if (evento != null)
        {
            List<Servicio> servicios = _servicioDao.GetServiciosByEvent(eventoId);
            return new ServiciosByEventoResponseDto()
            {
                Success = true,
                Servicios = servicios
            };
        }

        return new ServiciosByEventoResponseDto()
        {
            Success = false
        };
    }
}
