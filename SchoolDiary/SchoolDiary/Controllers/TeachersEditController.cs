using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolDiary.Domain.Models.Teacher;
using SchoolDiary.Domain.Services;
using System.Threading.Tasks;

namespace SchoolDiary.Controllers
{
    /// <summary>
    /// This controller provides possibility
    /// to work with teachers.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "admin")]
    public class TeachersEditController : Controller
    {
        private readonly ITeachersEditService _teachersEditService;
        public TeachersEditController(ITeachersEditService teachersEditService)
        {
            _teachersEditService = teachersEditService;
        }
        /// <summary>
        /// Gets all teachers from
        /// database 'Teachers' table.
        /// </summary>
        /// <returns>All teachers data.</returns>
        [HttpGet("GetAllTeachers")]
        public IActionResult GetAllTeachers()
        {
            var teachers = _teachersEditService.GetAll();
            return Ok(teachers);
        }
        /// <summary>
        /// Gets teacher by id.
        /// </summary>
        /// <param name="id">Teacher Id.</param>
        /// <returns>Teacher data.</returns>
        [HttpGet("GetTeacher")]
        public async Task<IActionResult> GetTeacher(int id)
        {
            var teacher = await _teachersEditService.GetByIdAsync(id);
            if (teacher == null)
            {
                ModelState.AddModelError("Error", "Учитель не найден.");
                return BadRequest(ModelState);
            }
            return Ok(teacher);
        }
        /// <summary>
        /// Deletes teacher from database
        /// 'Users' table.
        /// </summary>
        /// <param name="id">Teacher Id in 'Teachers' table.</param>
        /// <returns>Result of deleting teacher.</returns>
        [HttpDelete("DeleteTeacher")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var deletedTeacher = await _teachersEditService.DeleteByIdAsync(id);
            if (deletedTeacher == null)
            {
                ModelState.AddModelError("Error", "Учитель не найден!");
                return BadRequest(ModelState);
            }
            return Ok();
        }
        /// <summary>
        /// Edit's teacher entity in database
        /// 'Teachers' table.
        /// </summary>
        /// <param name="model">Contains 'Teachers' columns for editing.</param>
        /// <returns>Result of teacher editing.</returns>
        [HttpPut("ChangeTeacher")]
        public async Task<IActionResult> ChangeTeacher(EditTeacherModel model)
        {
            if (ModelState.IsValid)
            {
                var editedTeacher = await _teachersEditService.ChangeTeacherAsync(model);
                if (editedTeacher == null)
                {
                    ModelState.AddModelError("Error", "Не удалось изменить пользователя!");
                    return BadRequest(ModelState);
                }
                return Ok();
            }
            return BadRequest(ModelState);
        }
        /// <summary>
        /// Gets collection of classes, which pinned to concrete teacher.
        /// </summary>
        /// <param name="teacherId">Teacher Id in database 'Teachers' table.</param>
        /// <returns>IEnumerable, containing classes.</returns>
        [HttpGet("GetPinnedClassesByTeacherId")]
        public IActionResult GetPinnedClassesByTeacherId(int teacherId)
        {
            var classes = _teachersEditService.GetPinnedClassesByTeacherId(teacherId);
            if (classes != null)
            {
                return Ok(classes);
            }
            return BadRequest("Не удалось получить список предметов.");
        }
    }
}
