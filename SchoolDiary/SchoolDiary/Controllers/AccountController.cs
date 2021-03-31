using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolDiary.Domain.Data;
using SchoolDiary.Domain.Models.Authentication;
using SchoolDiary.Domain.Services;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Controllers
{
    /// <summary>
    /// This controller manages the authentication logic.
    /// At this project authenticate system realized with 
    /// JWT tokens.
    /// </summary>
    [Authorize]
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
        /// <summary>
        /// Provides way to authenticate for users.
        /// </summary>
        /// <param name="model">User entered data from account,
        /// come from the frontent part.</param>
        /// <returns>Result of authenticate.</returns>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]LoginModel model)
        {
            var user = _accountService.Authenticate(model.Login, model.Password);
            if (user != null)
            {
                return Ok(user);
            }
            return BadRequest(new { message = "Неверный логин или пароль." });
        }
        /// <summary>
        /// Registers new student.
        /// </summary>
        /// <param name="model">Entered data containes student information,
        /// come from the frontent part.</param>
        /// <returns>Result of new student register.</returns>
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
        /// <summary>
        /// Registers new teacher.
        /// </summary>
        /// <param name="model">Entered data containes teacher information,
        /// come from the frontent part.</param>
        /// <returns>Result of new teacher register.</returns>
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
