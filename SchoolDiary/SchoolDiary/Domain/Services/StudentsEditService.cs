using Microsoft.EntityFrameworkCore;
using SchoolDiary.Domain.Data;
using SchoolDiary.Domain.Data.Entities;
using SchoolDiary.Domain.Models.Authentication;
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
        Task<Student> ChangeStudentAsync(EditStudentModel model);
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
        /// Edits student data with new data from model.
        /// </summary>
        /// <param name="model">Represents new student data. Comes from frontend.</param>
        /// <returns></returns>
        public async Task<Student> ChangeStudentAsync(EditStudentModel model)
        {
            if (model.Id != 0)
            {
                var studentToChange = await _dbContext.Students
                    .Include(s => s.User)
                    .FirstOrDefaultAsync(s => s.Id == model.Id);
                if (studentToChange != null)
                {
                    studentToChange.User.Login = model.Login;
                    studentToChange.User.Firstname = model.Firstname;
                    studentToChange.User.Lastname = model.Lastname;
                    studentToChange.User.Patronymic = model.Patronymic;
                    studentToChange.User.Phone = model.Phone;
                    studentToChange.ClassId = model.ClassId;
                    await _dbContext.SaveChangesAsync();
                    return studentToChange;
                }
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
