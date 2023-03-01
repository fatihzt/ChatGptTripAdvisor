using ChatGptBot.Business.Request.User;
using ChatGptBot.Business.Response.User;
using ChatGptBot.Business.Service;
using ChatGptBot.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatGptBot.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegistirationRequest user)
        {
            var result = _userService.GetAll(u => u.Name == user.Name && u.Surname == user.Surname);
            if (result.Count >= 1) { return BadRequest("User is already exist."); }
            _userService.CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
            User entity = new()
            {
                Name = user.Name,
                Surname = user.Surname,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Mail = user.Mail
            };
            _userService.Add(entity);
            return Ok(entity);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginRequest user)
        {
            var result = _userService.GetAll(u => u.Mail == user.Mail);
            if (result.Count >= 1) { return BadRequest("user is already exist"); }
            foreach (var item in result)
            {
                if (!_userService.VerifyPasswordHash(user.Password, item.PasswordHash, item.PasswordSalt)) return BadRequest("wrong info");
                string token = _userService.CreateToken(item);
                UserLoginResponse loginResponse = new() { Mail = user.Mail, Password = user.Password };
                return Ok(loginResponse);
            }
            return Ok();

        }

    }
}
