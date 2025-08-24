using Microsoft.EntityFrameworkCore;
using RealEstateAPI.Data;
using RealEstateAPI.DTOs;
using RealEstateAPI.Models;
using System.Security.Cryptography;
using System.Text;

namespace RealEstateAPI.Services
{
    public interface IAuthService
    {
        Task<AuthResponse?> RegisterAsync(RegisterRequest request);
        Task<AuthResponse?> LoginAsync(LoginRequest request);
        Task<User?> GetUserByIdAsync(int userId);
    }

    public class AuthService : IAuthService
    {
        private readonly RealEstateDbContext _context;
        private readonly IJwtService _jwtService;

        public AuthService(RealEstateDbContext context, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        public async Task<AuthResponse?> RegisterAsync(RegisterRequest request)
        {
            // Check if user already exists
            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
            {
                return null;
            }

            var user = new User
            {
                Email = request.Email,
                PasswordHash = HashPassword(request.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var token = _jwtService.GenerateToken(user);
            return new AuthResponse
            {
                Token = token,
                Email = user.Email,
                UserId = user.Id
            };
        }

        public async Task<AuthResponse?> LoginAsync(LoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            
            if (user == null || !VerifyPassword(request.Password, user.PasswordHash))
            {
                return null;
            }

            var token = _jwtService.GenerateToken(user);
            return new AuthResponse
            {
                Token = token,
                Email = user.Email,
                UserId = user.Id
            };
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        private bool VerifyPassword(string password, string hash)
        {
            return HashPassword(password) == hash;
        }
    }
}
