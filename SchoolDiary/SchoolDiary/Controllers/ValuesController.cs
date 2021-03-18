using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SchoolDiary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        [Authorize]
        [Route("getLogin")]
        public IActionResult GetLogin()
        {
            return Ok($"Ваш логин: {User.Identity.Name}");
        }
        [Authorize(Roles = "admin")]
        [Route("getrole")]
        public IActionResult GetRole()
        {
            if (User.IsInRole("admin"))
            {
                return Ok($"Ваша роль: администратор");
            }
            return BadRequest("you are not admin!..");
        }
    }
}
