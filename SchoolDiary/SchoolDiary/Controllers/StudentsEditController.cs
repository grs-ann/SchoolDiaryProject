using Microsoft.AspNetCore.Mvc;
using SchoolDiary.Domain.Models.Student;
using SchoolDiary.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Controllers
{
    public class StudentsEditController : Controller
    {
        private readonly IStudentsEditService _studentsEditService;
        public StudentsEditController(IStudentsEditService studentsEditService)
        {
            _studentsEditService = studentsEditService;
        }
        public async Task<IActionResult> ChangeStudentClass(StudentsAndClassesModel model)
        {
            if (ModelState.IsValid)
            {
                var editedStudent = await _studentsEditService.ChangeClassForStudent(model);
                if (editedStudent != null)
                {
                    return Ok($"Класс ученика был изменен на {editedStudent.Class.Name}");
                }
                ModelState.AddModelError("Error", "Не удалось изменить класс для ученика.");
            }
            return BadRequest(ModelState);
        }
    }
}
