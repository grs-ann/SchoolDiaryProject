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
                //return Ok($"Учитель был изменён.");
                return Ok();
            }
            return BadRequest(ModelState);
        }
        /// <summary>
        /// Adds class to teacher by 
        /// teacher and subject id's.
        /// </summary>
        /// <param name="model">Contains teacher and class id's.</param>
        /// <returns>Result of class adding to teacher.</returns>
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
        /// <summary>
        /// Deletes class from teacher by 
        /// teacher and subject id's.
        /// </summary>
        /// <param name="model">Contains teacher and class id's.</param>
        /// <returns>Result of class deleting from teacher.</returns>
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
        /// <summary>
        /// Adds subject to teacher by 
        /// teacher and subject id's.
        /// </summary>
        /// <param name="model">Contains teacher and subject id's.</param>
        /// <returns>Result of subject adding to teacher.</returns>
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
        /// <summary>
        /// Deletes subject from teacher by 
        /// teacher and subject id's.
        /// </summary>
        /// <param name="model">Contains teacher and subject id's.</param>
        /// <returns>Result of subject deleting from teacher.</returns>
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
