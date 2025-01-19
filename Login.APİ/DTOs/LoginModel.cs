namespace Login.APİ.DTOs
{
    public record LoginModel
    {
        public string UsernameOrEmail { get; init; }
        public string Password { get; init; }
    }
}
