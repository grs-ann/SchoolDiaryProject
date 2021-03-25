using Microsoft.AspNetCore.Mvc;
using SchoolDiary.Domain.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VueTESTController : Controller
    {
        [HttpPost("userinfo")]
        public IActionResult UserInfo(TestUserModel model)
        {
            var res = model.Firstname + " " + model.Lastname;
            return Ok(res);
        }
    }
}
