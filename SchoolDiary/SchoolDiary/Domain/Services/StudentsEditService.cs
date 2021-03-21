using Microsoft.EntityFrameworkCore;
using SchoolDiary.Domain.Data;
using SchoolDiary.Domain.Data.Entities;
using SchoolDiary.Domain.Models.Student;
using SchoolDiary.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Domain.Services
{
    public interface IStudentsEditService : ICRUD<Student>
    {
        Task<Student> ChangeClassForStudent(StudentsAndClassesModel model);
    }
    public class StudentsEditService : IStudentsEditService
    {
        private readonly DataContext _dbContext;
        public StudentsEditService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Student> ChangeClassForStudent(StudentsAndClassesModel model)
        {
            var _class = await _dbContext.Classes
                .FirstOrDefaultAsync(c => c.Id == model.ClassId);
            var student = await GetByIdAsync(model.StudentId);
            if (student != null)
            {
                student.Class = _class;
                await _dbContext.SaveChangesAsync();
                return student;
            }
            return null;
        }
        public async Task<Student> DeleteByIdAsync(int id)
        {
            var student = await _dbContext.Students
                .FirstOrDefaultAsync(t => t.Id == id);
            return student;
        }
        public IEnumerable<Student> GetAll()
        {
            var students = _dbContext.Students;
            return students;
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            var student = await _dbContext.Students
                .Include(c => c.Class)
                .FirstOrDefaultAsync(t => t.Id == id);
            return student;
        }
    }
}
