using Microsoft.AspNetCore.Mvc;
using SchoolDiary.Domain.Data.Entities;
using SchoolDiary.Domain.Models.Class;
using SchoolDiary.Domain.Services;
using SchoolDiary.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassController : Controller
    {
        private readonly IClassService _classService;
        public ClassController(IClassService classService)
        {
            _classService = classService;
        }
        [HttpGet("getallclasses")]
        public IActionResult GetAllClasses()
        {
            var users = _classService.GetAll();
            return Ok(users);
        }
        [HttpGet("getclass")]
        public async Task<IActionResult> GetClass(int id)
        {
            var _class = await _classService.GetById(id);
            if (_class == null)
            {
                ModelState.AddModelError("Error", "Выбранного класса не существует");
                return BadRequest(ModelState);
            }
            return Ok(_class);
        }
        [HttpPost("AddNewClass")]
        public async Task<IActionResult> AddNewClass(ClassModel model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                ModelState.AddModelError("Error", "Название для класса не должно быть пустым.");
            }
            if (ModelState.IsValid)
            {
                await _classService.AddNewClassAsync(model);
                return Ok("Новый класс добавлен.");
            }
            return BadRequest(ModelState);
        }

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
                ModelState.AddModelError("Error", "Класс, который вы хотите изменить, не существует.");
            }
            return BadRequest(ModelState);
        }
    }
}
