using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolDiary.Domain.Models.Class;
using SchoolDiary.Domain.Services;
using System.Threading.Tasks;

namespace SchoolDiary.Controllers
{
    /// <summary>
    /// This controller manages the main student class-related CRUD operations.
    /// Only the school administrator has access to this controller.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "admin")]
    public class ClassController : Controller
    {
        private readonly IClassService _classService;
        private readonly IScheduleEditService _scheduleEditService;
        public ClassController(IClassService classService, IScheduleEditService scheduleEditService)
        {
            _classService = classService;
            _scheduleEditService = scheduleEditService;
        }
        /// <summary>
        /// Gets all classes from database 'Classes' table.
        /// </summary>
        /// <returns>IEnumerable collection of classes.</returns>
        [HttpGet("GetAllClasses")]
        public IActionResult GetAllClasses()
        {
            var users = _classService.GetAll();
            return Ok(users);
        }
        /// <summary>
        /// Gets concrete class from 
        /// database 'Classes' table.
        /// </summary>
        /// <param name="model">'ClassModel' contained class Id in database 'Classes' table.</param>
        /// <returns>Concrete class.</returns>
        [HttpGet("GetClass")]
        public async Task<IActionResult> GetClass(ClassModel model)
        {
            var _class = await _classService.GetByIdAsync(model.Id);
            if (_class == null)
            {
                ModelState.AddModelError("Error", "Выбранного класса не существует");
                return BadRequest(ModelState);
            }
            return Ok(_class);
        }
        /// <summary>
        /// Adds a new class to database 'Classes' table.
        /// </summary>
        /// <param name="name">Name of adding class.</param>
        /// <returns></returns>
        [HttpPost("AddNewClass")]
        public async Task<IActionResult> AddNewClass(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                ModelState.AddModelError("Error", "Название добавляемого класса не должно быть пустым.");
            }
            if (ModelState.IsValid)
            {
                var addedClass = await _classService.AddNewClassAsync(name);
                if (addedClass != null)
                {
                    return Ok($"Класс `{addedClass.Name}` добавлен.");
                }
                ModelState.AddModelError("Error", "Класс с таким названием уже существует!");
            }
            return BadRequest(ModelState);
        }
        /// <summary>
        /// Edits a class contained in database 'Classes' table.
        /// </summary>
        /// <param name="model">'ClassModel' contained class name in database 'Classes' table.</param>
        /// <returns></returns>
        [HttpPut("EditClass")]
        public IActionResult EditClass(ClassModel model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                ModelState.AddModelError("Error", "Название для класса не должно быть пустым.");
            }
            if (ModelState.IsValid)
            {
                var res = _classService.EditClassAsync(model);
                if (res != null)
                {
                    return Ok($"Название класса было изменено на {model.Name}");
                }
                ModelState.AddModelError("Error", "Класса, который вы хотите изменить, не существует.");
            }
            return BadRequest(ModelState);
        }
        /// <summary>
        /// Deletes a class contained in database 'Classes' table.
        /// </summary>
        /// <param name="model">'ClassModel' contained class Id in database 'Classes' table.</param>
        /// <returns></returns>
        [HttpDelete("DeleteClass")]
        public async Task<IActionResult> DeleteClass(ClassModel model)
        {
            var deletedClass =  await _classService.DeleteByIdAsync(model.Id);
            if (ModelState.IsValid)
            {
                if (deletedClass != null)
                {
                    return Ok($"Класс `{model.Name}` был удалён.");
                }
                ModelState.AddModelError("Error", "Невозможно удалить класс, " +
                    "так как его не существует!");
            }
            return BadRequest(ModelState);
        }
    }
}
