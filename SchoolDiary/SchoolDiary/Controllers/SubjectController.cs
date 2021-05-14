using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolDiary.Domain.Models.Authentication;
using SchoolDiary.Domain.Services;

namespace SchoolDiary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SubjectController : Controller
    {
        private readonly ISubjectService _subjectService;
        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }
        [HttpGet(nameof(GetAll))]
        public IActionResult GetAll()
        {
            var subjects = _subjectService.GetAll();
            if (subjects != null)
            {
                return Ok(subjects);
            }
            ModelState.AddModelError("Error", "Не удалось получить коллекцию предметов.");
            return BadRequest(ModelState);
        }
        [HttpGet(nameof(GetPinnedSubjectsByTeacherId))]
        public IActionResult GetPinnedSubjectsByTeacherId(int teacherId)
        {
            if (teacherId < 0)
            {
                ModelState.AddModelError("Error", "Недопустимое значение Id в таблице базы данных.");
                return BadRequest(ModelState);
            }
            var pinnedSubjects = _subjectService.GetPinnedSubjectsByTeacherId(teacherId);
            if (pinnedSubjects == null)
            {
                ModelState.AddModelError("Error", "Не удалось получить коллекцию закрепленных предметов");
            }
            return Ok(pinnedSubjects);
        }
    }
}
