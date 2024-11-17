using bailable_api.Database;
using bailable_api.Dtos;
using bailable_api.Models;

namespace bailable_api.Service;

public interface IEventoService
{
    public List<EventsByDateResponseDto> GetEventsByDate(DateTime date);
    public CreateEventoResponseDto CreateEvento(CreateEventoRequestDto createEventoRequestDto);
}
public class EventoService : IEventoService
{
    private readonly EventoDao _eventoDao;
    private readonly LocalDao _localDao;
    public EventoService(ContextDb contextDb)
    {
        _eventoDao = new EventoDao(contextDb);
        _localDao = new LocalDao(contextDb);
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
                Error = "Local no encontrado"
            };
        }
        if (createEventoRequestDto.Fecha <= DateTime.Now)
        {
            return new CreateEventoResponseDto()
            {
                Success = false,
                Error = "Ingrese una fecha valida."
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
                    Error = "Error al crear el evento"
                };
            }
            return new CreateEventoResponseDto()
            {
                Success = true,
                Error = "Evento creado correctamente."
            };
        }
        return new CreateEventoResponseDto()
        {
            Success = false,
            Error = "La fecha seleccionada no se encuentra disponible."
        };
    }

    private bool checkDisponibilidad(List<Evento> eventos, DateTime fecha)
    {
        Evento eventoEncontrado = eventos.FirstOrDefault(e => e.Fecha == fecha);
        bool result = eventoEncontrado != null ? false : true;
        return result;
    }
}
