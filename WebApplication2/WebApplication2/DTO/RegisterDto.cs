using System.ComponentModel.DataAnnotations;

namespace WebApplication2.DTO
{
    public class RegisterDto
    {
        [Required]
        [MaxLength(30,ErrorMessage = "Długość do 30")]
        public string Login { get; set; }
        [Required]
        [MaxLength(30,ErrorMessage = "Długość do 30")]
        public string Email { get; set; }
        [Required]
        [MaxLength(30,ErrorMessage = "Długość do 30")]
        public string Password { get; set; }
    }
}