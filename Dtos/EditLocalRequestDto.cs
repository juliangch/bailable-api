namespace bailable_api.Dtos
{
    public class EditLocalRequestDto
    {
        public required Guid UserId { get; set; }
        public required Models.Local Local {  get; set; }

    }
}
