namespace bailable_api.Dtos
{
    public class RegisterUserRequestDto
    {
        public Guid User_id { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required int Role {  get; set; }
    }
}
