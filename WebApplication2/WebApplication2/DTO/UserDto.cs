using System;

namespace WebApplication2.DTO
{
    public class UserDto
    {
        public int IdUser { get; set; }
        public string Login { get; set; }
        public string HashedPassword { get; set; }
        public Guid? RefreshToken { get; set; }
    }
}