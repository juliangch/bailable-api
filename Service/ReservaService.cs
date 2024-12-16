using bailable_api.Database;
using bailable_api.Dtos;
using bailable_api.Models;

namespace bailable_api.Service;

public interface IReservaService
{
    public CreateReservaResponseDto CreateReserva(CreateRerservaRequestDto createRerservaRequestDto);
    public ReservasByUserResponseDto GetReservasByUserId(Guid userId);
}
public class ReservaService : IReservaService
{
    private readonly ReservaDao _reservaDao;
    private readonly EventoDao _eventoDao;
    private readonly UserDao _userDao;
    private readonly ServicioDao _servicioDao;
    public ReservaService(ContextDb contextDb)
    {

        _reservaDao = new ReservaDao(contextDb);
        _eventoDao = new EventoDao(contextDb);
        _userDao = new UserDao(contextDb);
        _servicioDao = new ServicioDao(contextDb);

    }

    public CreateReservaResponseDto CreateReserva(CreateRerservaRequestDto createRerservaRequestDto)
    {
        User usuario = _userDao.GetUserById(createRerservaRequestDto.UserId);
        if (usuario == null)
        {
            return new CreateReservaResponseDto()
            {
                Success = false,
                Message = "Usuario no encontrado.",

            };
        }
        if (usuario.Role != Constants.UserRoleEnum.PARTICULAR)
        {
            return new CreateReservaResponseDto()
            {
                Success = false,
                Message = "Usuario no tiene permiso.",

            };

        }
        EventoWCapacidadDto evento = _eventoDao.GetEventoById(createRerservaRequestDto.EventoId);
        if (evento == null)
        {
            return new CreateReservaResponseDto()
            {
                Success = false,
                Message = "Evento no encontrado .",

            };
        }
        if (!CalcularCapacidad(evento, createRerservaRequestDto.CantidadPersonas))
        {
            return new CreateReservaResponseDto()
            {
                Success = false,
                Message = "Evento sin capacidad suficiente.",

            };
        }
        createRerservaRequestDto.Precio = CalcularPrecioReserva(createRerservaRequestDto.ServiciosIds) * createRerservaRequestDto.CantidadPersonas;
        createRerservaRequestDto.User = usuario;
        createRerservaRequestDto.Evento = evento.Evento;

        foreach (Guid servicioId in createRerservaRequestDto.ServiciosIds)
        {
            Servicio servicioAdquirido = _servicioDao.GetServicioById(servicioId);
            if (servicioAdquirido != null)
            {
                createRerservaRequestDto.Servicios.Add(servicioAdquirido);
            }
        }

        Reserva reserva = _reservaDao.CreateReserva(createRerservaRequestDto);
        if (reserva != null)
        {
            return new CreateReservaResponseDto()
            {
                Success = true,
                Message = "Reserva generada con exito.",

            };
        }
        return new CreateReservaResponseDto()
        {
            Success = false,
            Message = "Error al generar la reserva.",

        };
    }
    public ReservasByUserResponseDto GetReservasByUserId(Guid userId)
    {
        List<ReservaDetailsDto> reservas = _reservaDao.GetReservasByUserId(userId);
        if (reservas != null)
        {
            return new ReservasByUserResponseDto()
            {
                Success = true,
                Reservas = reservas
            };
        }
        else
        {
            return new ReservasByUserResponseDto()
            {
                Success = false,
            };
        }
    }
    private float CalcularPrecioReserva(List<Guid> serviciosSeleccionados)
    {
        float precioReserva = 0;
        foreach (Guid servicioId in serviciosSeleccionados)
        {
            Servicio servicio = _servicioDao.GetServicioById(servicioId);
            precioReserva += servicio.Precio;
        }
        return precioReserva;
    }

    private bool CalcularCapacidad(EventoWCapacidadDto evento, int cantPersonas)
    {
        int cantReservasEvento = _reservaDao.GetReservasByEvento(evento.Evento.EventoId);

        if (evento.Capacidad - cantReservasEvento < cantPersonas)
        {
            return false;
        }
        return true;
    }
}
