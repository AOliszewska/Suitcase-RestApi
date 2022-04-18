using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication2.DTO;
using WebApplication2.Models;
using WebApplication2.Repositories.Interfaces;

namespace WebApplication2.Repositories.Implementations
{
    public class AccountDbRepository : IAccountDbRepository
    {
        private readonly VacationContext _context;
        public AccountDbRepository(VacationContext context)
        {
            _context = context;
        }

        public async Task<string> RegisterUser(RegisterDto registerDto)
        {
            var login = await _context.Users.SingleOrDefaultAsync(e => e.Login == registerDto.Login);
            if (login != null)
            {
                return "Login is taken";
            }
            var account = new UserVacation();
            account.Login = registerDto.Login;
            account.HashedPassword = new PasswordHasher<UserVacation>().HashPassword(account, registerDto.Password);

            await _context.AddAsync(new UserVacation()
            {
                Login = registerDto.Login,
                HashedPassword = account.HashedPassword,
                Email = registerDto.Email,
                RefreshToken =  Guid.NewGuid().ToString()
            });

            await _context.SaveChangesAsync();

            return "A new user has been registered";
        }

        public async Task<UserDto> GetUser(LoginDto loginDto)
        {
            var account = await _context.Users.SingleOrDefaultAsync(x => x.Login == loginDto.Login);
            if (account == null)
            {
                return null;
            }
            var newRefreshToken = Guid.NewGuid();
            account.RefreshToken = newRefreshToken.ToString();
            
            await _context.SaveChangesAsync();
            
            return new UserDto
            {
                IdUser = account.IdUser,
                Login = account.Login,
                HashedPassword = account.HashedPassword,
                RefreshToken = newRefreshToken
            };
            
        }
    }
}