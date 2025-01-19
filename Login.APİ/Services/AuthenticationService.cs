using Login.APİ.DbContext;
using Login.APİ.Entities;
using Microsoft.EntityFrameworkCore;

namespace Login.APİ.Services
{
    public class AuthenticationService
    {
        private readonly MyContext _context;
        private readonly JwtService _jwtService;

        public AuthenticationService(MyContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        public async Task<string> LoginAsync(string usernameOrEmail, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == usernameOrEmail || u.Email == usernameOrEmail);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid username or password.");
            }

            var token = _jwtService.GenerateJwtToken(user);
            return token;
        }

        public async Task RegisterAsync(string username, string email, string password)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            var user = new User
            {
                Username = username,
                Email = email,
                PasswordHash = passwordHash
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}
