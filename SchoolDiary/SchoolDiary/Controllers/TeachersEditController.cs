using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolDiary.Domain.Models.Teacher;
using SchoolDiary.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Controllers
{
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
        [HttpGet("GetAllTeachers")]
        public IActionResult GetAllTeachers()
        {
            var teachers = _teachersEditService.GetAll();
            return Ok(teachers);
        }
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
        [HttpPut("EditTeacher")]
        public async Task<IActionResult> EditTeacher(TeacherModel model)
        {
            var editedTeacher = await _teachersEditService.EditTeacherAsync(model);
            if (editedTeacher == null)
            {
                ModelState.AddModelError("Error", "Не удалось изменить пользователя!");
                return BadRequest(ModelState);
            }
            return Ok($"Учитель был изменён.");
        }
        [HttpPost("AddClassToTeacher")]
        public async Task<IActionResult> AddClassToTeacher(TeachersAndClassesModel model)
        {
            if (ModelState.IsValid)
            {
                var res = await _teachersEditService.AddClassToTeacher(model.TeacherId, model.ClassId);
                if (res == null)
                {
                    ModelState.AddModelError("Error", "Класс не был добавлен к учителю.");
                    return BadRequest(ModelState);
                }
                return Ok("Класс добавлен к учителю.");
            }
            return BadRequest(ModelState);
        }
        [HttpDelete("DeleteClassFromTeacher")]
        public async Task<IActionResult> DeleteClassFromTeacher(TeachersAndClassesModel model)
        {
            if (ModelState.IsValid)
            {
                var res = await _teachersEditService.DeleteClassForTeacher(model.TeacherId, model.ClassId);
                if (res == null)
                {
                    ModelState.AddModelError("Error", "Не удалось удалить класс у учителя.");
                    return BadRequest(ModelState);
                }
                return Ok($"Класс был удален у учителя.");
            }
            return BadRequest(ModelState);
        }
        [HttpPost("AddSubjectToTeacher")]
        public async Task<IActionResult> AddSubjectToTeacher(TeachersAndSubjectsModel model)
        {
            if (ModelState.IsValid)
            {
                var res = await _teachersEditService.AddSubjectToTeacher(model.TeacherId, model.SubjectId);
                if (res == null)
                {
                    ModelState.AddModelError("Error", "Предмет не был добавлен к учителю.");
                    return BadRequest(ModelState);
                }
                return Ok("Предмет добавлен к учителю.");
            }
            return BadRequest(ModelState);
        }
        [HttpDelete("DeleteSubjectFromTeacher")]
        public async Task<IActionResult> DeleteSubjectFromTeacher(TeachersAndSubjectsModel model)
        {
            if (ModelState.IsValid)
            {
                var res = await _teachersEditService.DeleteSubjectFromTeacher(model.TeacherId, model.SubjectId);
                if (res == null)
                {
                    ModelState.AddModelError("Error", "Не удалось удалить предмет у учителя.");
                    return BadRequest(ModelState);
                }
                return Ok($"Предмет был удален у учителя.");
            }
            return BadRequest(ModelState);
        }
    }
}
