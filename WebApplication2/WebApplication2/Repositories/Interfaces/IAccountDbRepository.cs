using System.Threading.Tasks;
using WebApplication2.DTO;

namespace WebApplication2.Repositories.Interfaces
{
    public interface IAccountDbRepository
    {
        Task<string> RegisterUser(RegisterDto register);
        Task<UserDto> GetUser(LoginDto loginDto);
    }
}