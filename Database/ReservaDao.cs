using bailable_api.Dtos;
using bailable_api.Models;

namespace bailable_api.Database;

public class ReservaDao
{
    private readonly ContextDb _contextDb;

    public ReservaDao(ContextDb contextDb)
    {
        _contextDb = contextDb;
    }


    public Reserva CreateReserva(CreateRerservaRequestDto rerservaRequestDto)
    {
        Reserva reserva = new Reserva()
        {
            Usuario = rerservaRequestDto.User,
            Evento = rerservaRequestDto.Evento,
            Servicios = rerservaRequestDto.Servicios,
            CantidadPersonas = rerservaRequestDto.CantidadPersonas,
            Precio = rerservaRequestDto.Precio
        };
        _contextDb.Add(reserva);
        _contextDb.SaveChanges();
        return reserva;
    }

    public int GetReservasByEvento(Guid eventoId)
    {
        int totalPersonas = _contextDb.Reservas
        .Where(r => r.Evento.EventoId == eventoId)
        .Sum(r => r.CantidadPersonas);

        return totalPersonas;
    }
    public List<ReservaDetailsDto> GetReservasByUserId(Guid userId)
    {
        List<ReservaDetailsDto> reservas = _contextDb.Reservas.Where(r => r.Usuario.UserId == userId).Select(r => new ReservaDetailsDto
        {
            Precio = r.Precio,
            NombreEvento = r.Evento.Nombre,
            Fecha = r.Evento.Fecha,
            CantidadPersonas = r.CantidadPersonas
        }).ToList();
        return reservas;
    }
}
