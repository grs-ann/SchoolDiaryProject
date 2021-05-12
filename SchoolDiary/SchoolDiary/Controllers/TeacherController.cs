using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolDiary.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Controllers
{
    /// <summary>
    /// This controller provides possibility 
    /// for teacher to control study process management.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "teacher")]
    public class TeacherController : Controller
    {
        ITeacherService _teacherService;
        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }
        /// <summary>
        /// Gets collection of classes, which 
        /// pinned to concrete teacher.
        /// </summary>
        /// <param name="teacherId">Teacher id in database table.</param>
        /// <returns>Result of query.</returns>
        [HttpGet(nameof(GetPinnedClasses))]
        public IActionResult GetPinnedClasses(int teacherId)
        {
            if (teacherId <= 0)
            {
                ModelState.AddModelError("Error", "Передано некорректное Id для учителя.");
            }
            if (ModelState.IsValid)
            {
                var pinnedClasses = _teacherService.GetPinnedClasses(teacherId);
                return Ok(pinnedClasses);
            }
            return BadRequest(ModelState);
        }
    }
}
