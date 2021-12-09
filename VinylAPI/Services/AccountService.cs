using Microsoft.AspNetCore.Identity;
using VinylAPI.Data;
using VinylAPI.Entities;
using VinylAPI.Models;

namespace VinylAPI.Services
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto dto);
    }
    public class AccountService : IAccountService
    {
        private readonly VinylAPIDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AccountService(VinylAPIDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

         public void RegisterUser(RegisterUserDto dto)
        {
            var newUser = new User()
            {
                Email = dto.Email,
                BirthDay = dto.BirthDay,
                Name = dto.Name,
                SurrName = dto.SurrName,
                Nick = dto.Nick,
                RoleId = 1,
            };
            var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);
            newUser.PasswordHash = hashedPassword;
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }
    }
}
