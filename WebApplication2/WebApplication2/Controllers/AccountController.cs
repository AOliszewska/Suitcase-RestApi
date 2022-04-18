using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.DTO;
using WebApplication2.Repositories.Interfaces;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly IAccountDbRepository _accountDbRepository;
        public AccountController(IAccountDbRepository accountDbRepository)
        {
            _accountDbRepository = accountDbRepository;
        }
        
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDto register)
        {
            var res = await _accountDbRepository.RegisterUser(register);
            if (res == "Login is taken")
            {
                BadRequest(res);
            }
            return Ok(res);
        }
        
        [HttpPut]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var userfromDb = await _accountDbRepository.GetUser(loginDto);
            if (userfromDb == null) {
                return NotFound("User not found");
            }
            var password = new PasswordHasher<LoginDto>().VerifyHashedPassword(loginDto,
                userfromDb.HashedPassword, loginDto.Password);
            
            if (password != PasswordVerificationResult.Success){
                return BadRequest("The passwords do not match");
            }
        
            return Ok(userfromDb);

        }
    }
}