using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolDiary.Domain.Data.Entities;
using SchoolDiary.Domain.Services;
using SchoolDiary.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IBaseCRUDOperations<User> _userService;
        public UsersController(IBaseCRUDOperations<User> userService)
        {
            _userService = userService;
        }
        [HttpGet("getallusers")]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
    }
}
