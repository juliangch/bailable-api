namespace bailable_api.Dtos;

public class ReservasByUserResponseDto
{
    public bool Success { get; set; }
    public List<ReservaDetailsDto> Reservas { get; set; } = new List<ReservaDetailsDto>();
}
