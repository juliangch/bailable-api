namespace bailable_api.Dtos
{
    public class AuthenticateUserRequestDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
