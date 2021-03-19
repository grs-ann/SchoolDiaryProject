﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolDiary.Domain.Data;
using SchoolDiary.Domain.Models.Authentication;
using SchoolDiary.Domain.Services;
using System.Linq;
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
        [HttpPost("RegisterStudent")]
        public async Task<IActionResult> RegisterStudent(RegisterStudentModel model)
        {
            if (_dbContext.Users.Any(u => u.Login == model.Login))
            {
                ModelState.AddModelError("Error", "Аккаунт с таким логином уже существует.");
            }
            if (ModelState.IsValid)
            {
                await _accountService.RegisterStudentAsync(model);
                return Ok();
            }
            return BadRequest(ModelState);
        }
        [Authorize(Roles = "admin")]
        [HttpPost("RegisterTeacher")]
        public async Task<IActionResult> RegisterTeacher(RegisterTeacherModel model)
        {
            if (_dbContext.Users.Any(u => u.Login == model.Login))
            {
                ModelState.AddModelError("Error", "Аккаунт с таким логином уже существует.");
            }
            if (ModelState.IsValid)
            {
                await _accountService.RegisterTeacherAsync(model);
                return Ok();
            }
            return BadRequest(ModelState);
        }
    }
}
