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
    public Evento CreateEvento(CreateEventoRequestDto createEventoRequestDto)
    {
        Evento evento = new Evento()
        {
            Nombre = createEventoRequestDto.Nombre,
            Descripcion = createEventoRequestDto.Descripcion,
            Fecha = createEventoRequestDto.Fecha,
            Servicios = createEventoRequestDto.Servicios,
            ImgSource = createEventoRequestDto.ImgSource,
            Local = createEventoRequestDto.Local
        };
        _dbContext.Eventos.Add(evento);
        _dbContext.SaveChanges();
        return evento;
    }
    public EventoWCapacidadDto GetEventoById(Guid id)
    {
        EventoWCapacidadDto evento = _dbContext.Eventos.Where(e => e.EventoId == id).Select(e => new EventoWCapacidadDto
        {
            Evento = e,
            Capacidad = e.Local.Capacidad,
        }).FirstOrDefault();
        return evento;
    }

    public Evento DeleteEvento(Guid id)
    {
        var eventoToChange = _dbContext.Eventos.SingleOrDefault(e =>  e.EventoId == id);
        if (eventoToChange != null) 
        {
            eventoToChange.DeletedAt = DateTime.Now;
            _dbContext.SaveChanges();
        }
        return eventoToChange;
    }
}
