using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolDiary.Domain.Data;
using SchoolDiary.Domain.Models.Authentication;
using SchoolDiary.Domain.Services.Interfaces;
using SchoolDiary.Helpers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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
        [HttpPost("login")]
        public IActionResult Login(LoginModel model)
        {
            if (model != null)
            {
                var decodedJWT = _accountService.Login(model);
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
        [HttpPost("register")]
        [Authorize(Roles = "admin")]
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
