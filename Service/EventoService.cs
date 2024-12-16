using bailable_api.Database;
using bailable_api.Dtos;
using bailable_api.Models;

namespace bailable_api.Service;

public interface IEventoService
{
    public List<EventsByDateResponseDto> GetEventsByDate(DateTime date);
    public CreateEventoResponseDto CreateEvento(CreateEventoRequestDto createEventoRequestDto);
    public void DeleteEvento(CancelEventoRequestDto cancelEventoRequestDto);
    public List<Evento> GetEventosByLocalId(Guid localId);
}
public class EventoService : IEventoService
{
    private readonly EventoDao _eventoDao;
    private readonly LocalDao _localDao;
    private readonly ServicioDao _servicioDao;
    public EventoService(ContextDb contextDb)
    {
        _eventoDao = new EventoDao(contextDb);
        _localDao = new LocalDao(contextDb);
        _servicioDao = new ServicioDao(contextDb);
    }
    public List<EventsByDateResponseDto> GetEventsByDate(DateTime date)
    {
        List<EventsByDateResponseDto> events = _eventoDao.GetEventsByDate(date);
        return events;
    }
    public CreateEventoResponseDto CreateEvento(CreateEventoRequestDto createEventoRequestDto)
    {
        Local localEncontrado = _localDao.GetLocalByIdWEventos(createEventoRequestDto.LocalId);
        if (localEncontrado == null)
        {
            return new CreateEventoResponseDto()
            {
                Success = false,
                Message = "Local no encontrado"
            };
        }
        if (createEventoRequestDto.Fecha <= DateTime.Now)
        {
            return new CreateEventoResponseDto()
            {
                Success = false,
                Message = "Ingrese una fecha valida."
            };
        }
        if (checkDisponibilidad(localEncontrado.Eventos, createEventoRequestDto.Fecha))
        {
            createEventoRequestDto.Local = localEncontrado;
            Evento eventoCreado = _eventoDao.CreateEvento(createEventoRequestDto);
            if (eventoCreado == null)
            {
                return new CreateEventoResponseDto()
                {
                    Success = false,
                    Message = "Error al crear el evento"
                };
            }
            foreach (ServicioDto servicio in createEventoRequestDto.Servicios)
            {
                Servicio servicioToAdd = new Servicio()
                {
                    Nombre = servicio.Nombre,
                    Descripcion = servicio.Descripcion,
                    Precio = servicio.Precio,
                    Evento = eventoCreado
                };
                _servicioDao.CreateServicio(servicioToAdd);
            }
            return new CreateEventoResponseDto()
            {
                Success = true,
                Message = "Evento creado correctamente."
            };
        }
        return new CreateEventoResponseDto()
        {
            Success = false,
            Message = "La fecha seleccionada no se encuentra disponible."
        };
    }

    private bool checkDisponibilidad(List<Evento> eventos, DateTime fecha)
    {
        Evento eventoEncontrado = eventos.FirstOrDefault(e => e.Fecha == fecha);
        bool result = eventoEncontrado != null ? false : true;
        return result;
    }

    public void DeleteEvento(CancelEventoRequestDto cancelEventoRequestDto)
    {
        if (CheckIfAuthorized(cancelEventoRequestDto.UserId, cancelEventoRequestDto.EventoId))
        {
            var result = _eventoDao.DeleteEvento(cancelEventoRequestDto.EventoId);

        }
        else
        {
            throw new Exception("No puede cancelar el evento");
        }
    }

    private bool CheckIfAuthorized(Guid userId, Guid eventoId)
    {
        throw new NotImplementedException();
    }
    public List<Evento> GetEventosByLocalId(Guid localId)
    {
        List<Evento> eventosByLocal = new List<Evento>();
        eventosByLocal = _eventoDao.GetEventoByLocalId(localId);
        return eventosByLocal;
    }
}
