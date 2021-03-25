using Microsoft.EntityFrameworkCore;
using SchoolDiary.Domain.Data;
using SchoolDiary.Domain.Data.Entities;
using SchoolDiary.Domain.Models.Teacher;
using SchoolDiary.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Domain.Services
{
    /// <summary>
    /// This interface represents operations
    /// for work with database 'Teachers' and related with this tables.
    /// </summary>
    public interface ITeachersEditService : ICRUD<Teacher>
    {
        Task<TeachersClasses> AddClassToTeacher(int teacherId, int classId);
        Task<TeachersClasses> DeleteClassForTeacher(int teacherId, int classId);
        Task<TeachersSubjects> AddSubjectToTeacher(int teacherId, int subjectId);
        Task<TeachersSubjects> DeleteSubjectFromTeacher(int teacherId, int subjectId);
        Task<Teacher> EditTeacherAsync(TeacherModel model);
    }
    /// <summary>
    /// This service contains a set of methods 
    /// with logic for managing teachers.
    /// </summary>
    public class TeachersEditService : ITeachersEditService
    {
        private readonly DataContext _dbContext;
        public TeachersEditService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// Gets all teachers from database
        /// 'Teachers' table.
        /// </summary>
        /// <returns>All teachers.</returns>
        public IEnumerable<Teacher> GetAll()
        {
            var teachers = _dbContext.Teachers;
            return teachers;
        }
        /// <summary>
        /// Gets concrete Teacher by Id.
        /// </summary>
        /// <param name="id">Teacher Id.</param>
        /// <returns>Teacher.</returns>
        public async Task<Teacher> GetByIdAsync(int id)
        {
            var teacher = await _dbContext.Teachers
                .FirstOrDefaultAsync(t => t.Id == id);
            return teacher;
        }
        /// <summary>
        /// Edits teacher in database
        /// 'Teachers' table.
        /// </summary>
        /// <param name="model">Contains teacher Id and salary.</param>
        /// <returns>Edited teacher.</returns>
        public async Task<Teacher> EditTeacherAsync(TeacherModel model)
        {
            var teacher = await this.GetByIdAsync(model.Id);
            if (teacher != null)
            {
                teacher.Salary = model.Salary;
                await _dbContext.SaveChangesAsync();
                return teacher;
            }
            return null;
        }
        /// <summary>
        /// Deletes teacher(as User) from
        /// database 'Users', 'Teachers' tables by teacher Id.
        /// </summary>
        /// <param name="id">Teacher Id.</param>
        /// <returns>Deleted teacher.</returns>
        public async Task<Teacher> DeleteByIdAsync(int id)
        {
            var userToDelete = await _dbContext.Users.Include(u => u.Teacher).FirstOrDefaultAsync(u => u.Teacher.Id == id);
            if (userToDelete != null)
            {
                _dbContext.Users.Remove(userToDelete);
                await _dbContext.SaveChangesAsync();
                return userToDelete.Teacher;
            }
            return null;
        }
        /// <summary>
        /// Adds class to teacher by theirs ids.
        /// </summary>
        /// <param name="teacherId">Teacher Id.</param>
        /// <param name="classId">Class Id.</param>
        /// <returns></returns>
        public async Task<TeachersClasses> AddClassToTeacher(int teacherId, int classId)
        {
            var teacher = await _dbContext.Teachers
                .FirstOrDefaultAsync(t => t.Id == teacherId);
            var _class = await _dbContext.Classes
                .FirstOrDefaultAsync(c => c.Id == classId);
            if (teacher != null && _class != null)
            {
                var res = new TeachersClasses
                {
                    Teacher = teacher,
                    Class = _class
                };
                await _dbContext.TeachersClasses.AddAsync(res);
                await _dbContext.SaveChangesAsync();
                return res;
            }
            return null;
        }
        /// <summary>
        /// Deletes class for teacher by theirs ids.
        /// </summary>
        /// <param name="teacherId">Teacher Id.</param>
        /// <param name="classId">Class Id.</param>
        /// <returns>Deleted TeachersClasses item.</returns>
        public async Task<TeachersClasses> DeleteClassForTeacher(int teacherId, int classId)
        {
            var teacher = await _dbContext.Teachers
                .FirstOrDefaultAsync(t => t.Id == teacherId);
            var _class = await _dbContext.Classes
                .FirstOrDefaultAsync(c => c.Id == classId);
            if (teacher != null && _class != null)
            {
                var itemToDelete = await _dbContext.TeachersClasses
                    .FirstOrDefaultAsync(tc => tc.TeacherId == teacherId &&
                    tc.ClassId == classId);
                if (itemToDelete != null)
                {
                    _dbContext.Remove(itemToDelete);
                    return itemToDelete;
                }
            }
            return null;
        }
        /// <summary>
        /// Adds subject to teacher by theirs ids.
        /// </summary>
        /// <param name="teacherId">Teacher Id.</param>
        /// <param name="subjectId">Subject Id.</param>
        /// <returns>TeachersSubjects table item.</returns>
        public async Task<TeachersSubjects> AddSubjectToTeacher(int teacherId, int subjectId)
        {
            var teacher = await _dbContext.Teachers
                .FirstOrDefaultAsync(t => t.Id == teacherId);
            var subject = await _dbContext.Subjects
                .FirstOrDefaultAsync(c => c.Id == subjectId);
            if (teacher != null && subject != null)
            {
                var res = new TeachersSubjects
                {
                    Teacher = teacher,
                    Subject = subject
                };
                await _dbContext.TeachersSubjects.AddAsync(res);
                await _dbContext.SaveChangesAsync();
                return res;
            }
            return null;
        }
        /// <summary>
        /// Deletes subject from teacher by theirs ids.
        /// </summary>
        /// <param name="teacherId">Teacher Id.</param>
        /// <param name="subjectId">Subject Id.</param>
        /// <returns>Deleted TeachersSubjects item.</returns>
        public async Task<TeachersSubjects> DeleteSubjectFromTeacher(int teacherId, int subjectId)
        {
            var teacher = await _dbContext.Teachers
                .FirstOrDefaultAsync(t => t.Id == teacherId);
            var subject = await _dbContext.Subjects
                .FirstOrDefaultAsync(c => c.Id == subjectId);
            if (teacher != null && subject != null)
            {
                var itemToDelete = await _dbContext.TeachersSubjects
                    .FirstOrDefaultAsync(tc => tc.TeacherId == teacherId &&
                    tc.SubjectId == subjectId);
                if (itemToDelete != null)
                {
                    _dbContext.Remove(itemToDelete);
                    return itemToDelete;
                }
            }
            return null;
        }
    }
}
