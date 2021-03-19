using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolDiary.Domain.Data;
using SchoolDiary.Domain.Data.Entities;
using SchoolDiary.Domain.Models.TeachersSubjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Controllers
{
    [Authorize(Roles = "admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : Controller
    {
        private readonly DataContext _dbContext;
        public AdminController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpPost("AddSubjectToTeacher")]
        public IActionResult AddSubjectToTeacher(TeachersSubjectsModel model)
        {
            _dbContext.TeachersSubjects.Add(
                new TeachersSubjects
                {
                    SubjectId = model.SubjectId,
                    TeacherId = model.TeacherId
                });
            _dbContext.SaveChanges();
            return Ok();
        }
        [HttpGet("GetAllTeachers")]
        public IActionResult GetAllTeachers()
        {
            var teachers = _dbContext.Teachers;
            return Ok(teachers);
        }
    }
}
