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
    public interface ITeachersEditService : ICRUD<Teacher>
    {
        Task<TeachersClasses> AddClassToTeacher(int teacherId, int classId);
        Task<TeachersClasses> DeleteClassForTeacher(int teacherId, int classId);
        Task<TeachersSubjects> AddSubjectToTeacher(int teacherId, int subjectId);
        Task<TeachersSubjects> DeleteSubjectFromTeacher(int teacherId, int subjectId);
        Task<Teacher> EditTeacherAsync(TeacherModel model);
    }
    public class TeachersEditService : ITeachersEditService
    {
        private readonly DataContext _dbContext;
        public TeachersEditService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Teacher> GetAll()
        {
            var teachers = _dbContext.Teachers;
            return teachers;
        }
        public async Task<Teacher> GetByIdAsync(int id)
        {
            var teacher = await _dbContext.Teachers
                .FirstOrDefaultAsync(t => t.Id == id);
            return teacher;
        }
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
