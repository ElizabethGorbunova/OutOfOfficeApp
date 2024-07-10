using Microsoft.AspNetCore.Mvc;
using OutOfOfficeApp.Models;
using OutOfOfficeApp.Services;
using System.Runtime.CompilerServices;

namespace OutOfOfficeApp.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController:ControllerBase
    {
        private readonly IAccountService accountService;
        public AccountController(IAccountService _accountService)
        {
            accountService = _accountService;
        }
        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegisterUserDtoIn newUser)
        {
            accountService.RegisterUser(newUser);
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDtoIn login)
        {
            var token = accountService.GenerateJwt(login);
            return Ok(token);
        }
    }
}
