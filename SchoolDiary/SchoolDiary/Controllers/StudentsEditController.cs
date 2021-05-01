using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolDiary.Domain.Models.Student;
using SchoolDiary.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Controllers
{
    /// <summary>
    /// This controller provides possibility
    /// to work with students.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "admin")]
    public class StudentsEditController : Controller
    {
        private readonly IStudentsEditService _studentsEditService;
        public StudentsEditController(IStudentsEditService studentsEditService)
        {
            _studentsEditService = studentsEditService;
        }
        [HttpGet("GetAllStudents")]
        public IActionResult GetAllStudents()
        {
            var students = _studentsEditService.GetAll();
            if (students == null)
            {
                ModelState.AddModelError("Error", "Не найдено ни одного студента.");
                return BadRequest(ModelState);
            }
            return Ok(students);
        }
        [HttpPut("ChangeStudent")]
        public async Task<IActionResult> ChangeStudent(EditStudentModel model)
        {
            if (ModelState.IsValid)
            {
                var editedStudent = await _studentsEditService.ChangeStudentAsync(model);
                if (editedStudent != null)
                {
                    return Ok(editedStudent);
                }
            }
            return BadRequest(ModelState);
        }
    }
}
