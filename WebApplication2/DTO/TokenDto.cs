using System;

namespace WebApplication2.DTO
{
    public class TokenDto
    {
        public string Token { get; set; }
        public Guid? RefreshToken { get; set; }
    }
}