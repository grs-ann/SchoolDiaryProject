using Microsoft.AspNetCore.Mvc;
using SchoolDiary.Domain.Models.Authentication;

namespace SchoolDiary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VueTESTController : Controller
    {
        [HttpPost("UserInfo")]
        public IActionResult UserInfo(TestUserModel model)
        {
            var res = model.Firstname + " " + model.Lastname;
            return Ok(res);
        }
    }
}
