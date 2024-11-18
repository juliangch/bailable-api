namespace bailable_api.Dtos
{
    public class CancelEventoRequestDto
    {
        public Guid UserId { get; set; }
        public Guid EventoId { get; set; }
    }
}
