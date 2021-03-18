using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolDiary.Domain.Data;
using SchoolDiary.Domain.Models.Authentication;
using SchoolDiary.Domain.Services.Interfaces;
using System.Linq;

namespace SchoolDiary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly DataContext _dbContext; 
        private readonly IAccountService _accountService;
        public AccountController(DataContext dbContext, IAccountService accountService)
        {
            _dbContext = dbContext;
            _accountService = accountService;
        }
        [HttpPost("authenticate")]
        public IActionResult Authenticate(LoginModel model)
        {
            if (model != null)
            {
                var decodedJWT = _accountService.Authenticate(model);
                if (decodedJWT != null)
                {
                    return Json(new 
                    {
                        access_token = decodedJWT
                    });
                }
            }
            return BadRequest(new
            {
                errorText = "Неверный логин или пароль."
            });
        }
        [Authorize]
        [HttpPost("unauthenticate")]
        public IActionResult Unauthenticate()
        {
            _accountService.Unauthenticate();
            return Ok("Unauthorized!");
        }
        [Authorize(Roles = "admin")]
        [HttpPost("register")]
        public IActionResult Register(RegisterModel model)
        {
            if (_dbContext.Users.Any(u => u.Login == model.Login))
            {
                ModelState.AddModelError("Error", "Аккаунт с таким названием уже существует.");
            }
            if (ModelState.IsValid)
            {
                _accountService.Register(model);
                return Ok();
            }
            return BadRequest(ModelState);
        }
    }
}
