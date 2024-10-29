using bailable_api.Dtos;
using bailable_api.Models;
using Microsoft.EntityFrameworkCore;

namespace bailable_api.Database;

public class EventoDao
{
    private readonly ContextDb _dbContext;
    public EventoDao(ContextDb dbContext)
    {
        _dbContext = dbContext;
    }

    public List<EventsByDateResponseDto> GetEventsByDate(DateTime date)
    {
        List<EventsByDateResponseDto> events = _dbContext.Eventos.Where(e => e.Fecha <= date)
            .Include(e => e.Local)
            .ThenInclude(l => l.Zona)
            .Select(e => new EventsByDateResponseDto
            {
                Evento_id = e.EventoId,
                Nombre = e.Nombre,
                Descripcion = e.Descripcion,
                Date = e.Fecha,
                ImgSource = e.ImgSource,
            }).ToList();
        return events;
    }
}
