using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolDiary.Domain.Services;

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
        IMarkService _markService;
        public TeacherController(ITeacherService teacherService, IMarkService markService)
        {
            _teacherService = teacherService;
            _markService = markService;
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
        [HttpGet(nameof(GetStudentMarks))]
        public IActionResult GetStudentMarks(int studentId, int subjectId)
        {
            if (ModelState.IsValid)
            {
                var studentMarks = _markService.GetConcreteStudentMarksBySubjectId(studentId, subjectId);
                if (studentMarks != null)
                {
                    return Ok(studentMarks);
                }
                ModelState.AddModelError("Error", "Не удалось получить оценки ученика.");
                return BadRequest(ModelState);
            }
            return BadRequest(ModelState);
        }
    }
}
