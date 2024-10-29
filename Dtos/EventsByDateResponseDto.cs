namespace bailable_api.Dtos;

public class EventsByDateResponseDto
{
    public Guid Evento_id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public DateTime Date { get; set; }
    public string ImgSource { get; set; }
}
