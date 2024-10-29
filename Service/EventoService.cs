using bailable_api.Database;
using bailable_api.Dtos;
using bailable_api.Models;

namespace bailable_api.Service;

public interface IEventoService
{
    public List<EventsByDateResponseDto> GetEventsByDate(DateTime date);
}
public class EventoService : IEventoService
{
    private readonly EventoDao _eventoDao;
    public EventoService(ContextDb contextDb)
    {
        _eventoDao = new EventoDao(contextDb);
    }
    public List<EventsByDateResponseDto> GetEventsByDate(DateTime date)
    {
        List<EventsByDateResponseDto> events = _eventoDao.GetEventsByDate(date);
        return events;
    }
}
