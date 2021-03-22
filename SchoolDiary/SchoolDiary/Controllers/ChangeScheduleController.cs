using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "admin")]
    public class ChangeScheduleController : Controller
    {
        public ChangeScheduleController()
        {

        }
        /*public IActionResult ChangeConcreteSchedule()
        {

        }*/
    }
}
