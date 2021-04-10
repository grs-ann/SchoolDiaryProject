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
    /// <summary>
    /// This interface represents operations
    /// for work with database 'Students', 'Classes' tables.
    /// </summary>
    public interface IStudentsEditService : ICRUD<Student>
    {
        Task<Student> ChangeClassForStudent(StudentsAndClassesModel model);
    }
    /// <summary>
    /// This service contains a set of methods 
    /// with logic for managing students.
    /// </summary>
    public class StudentsEditService : IStudentsEditService
    {
        private readonly DataContext _dbContext;
        public StudentsEditService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// Changes class for concrete student.
        /// </summary>
        /// <param name="model">Contains student Id and class Id.</param>
        /// <returns>Changed student.</returns>
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

        public Task<Student> DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all students from database
        /// 'Students' table.
        /// </summary>
        /// <returns>All students.</returns>
        public IEnumerable<Student> GetAll()
        {
            var students = _dbContext.Students
                .Include(c => c.Class)
                .Include(x => x.User);
            return students;
        }
        /// <summary>
        /// Gets concrete student by Id.
        /// </summary>
        /// <param name="id">Student Id.</param>
        /// <returns>Student, which includes his class data.</returns>
        public async Task<Student> GetByIdAsync(int id)
        {
            var student = await _dbContext.Students
                .Include(c => c.Class)
                .FirstOrDefaultAsync(t => t.Id == id);
            return student;
        }
    }
}
