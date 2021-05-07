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
        IEnumerable<TeachersClasses> GetPinnedClassesByTeacherId(int teacherId);
        Task<Teacher> ChangeTeacherAsync(EditTeacherModel model);
        Task ChangePinnedClassesForTeacherAsync(int teacherId, IEnumerable<int> classIds);
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
            var teachers = _dbContext.Teachers
                .Include(u => u.User);
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
        /// Gets collection of classes, which pinned
        /// to concrete teacher.
        /// </summary>
        /// <param name="id">Teacher id.</param>
        /// <returns></returns>
        public IEnumerable<TeachersClasses> GetPinnedClassesByTeacherId(int id)
        {
            var classes = _dbContext.TeachersClasses
                .Where(tc => tc.TeacherId == id);
            return classes;
        }
        /// <summary>
        /// Edits teacher in database
        /// 'Teachers' table.
        /// </summary>
        /// <param name="model">Contains new teacher data.</param>
        /// <returns>Edited teacher.</returns>
        public async Task<Teacher> ChangeTeacherAsync(EditTeacherModel model)
        {
            if (model.Id != 0)
            {
                var teacherToChange = await _dbContext.Teachers
                    .Include(s => s.User)
                    .FirstOrDefaultAsync(s => s.Id == model.Id);
                if (teacherToChange != null)
                {
                    teacherToChange.User.Login = model.Login;
                    teacherToChange.User.Firstname = model.Firstname;
                    teacherToChange.User.Lastname = model.Lastname;
                    teacherToChange.User.Patronymic = model.Patronymic;
                    teacherToChange.User.Phone = model.Phone;
                    teacherToChange.Salary = model.Salary;
                    
                    if (model.ClassIds.Count >= 0)
                    {
                        var teacherId = teacherToChange.User.Teacher.Id;
                        await this.ChangePinnedClassesForTeacherAsync(teacherId, model.ClassIds);
                    }
                    await _dbContext.SaveChangesAsync();
                    return teacherToChange;
                }
            }
            return null;
        }
        /// <summary>
        /// Changes the bound classes to the teacher
        /// by his id.
        /// </summary>
        /// <param name="teacherId">Teacher id in database.</param>
        /// <param name="classIds">Class id in database.</param>
        /// <returns></returns>
        public async Task ChangePinnedClassesForTeacherAsync(int teacherId, IEnumerable<int> classIds)
        {
            var alreadyPinnedClasses = _dbContext.TeachersClasses
                .Where(tc => tc.TeacherId == teacherId)
                .AsEnumerable();
            var classIdsToDetach = alreadyPinnedClasses
                .Select(pc => pc.ClassId)
                .Except(classIds);
            var classIdsToPin = classIds
                .Except(alreadyPinnedClasses
                .Select(pc => pc.ClassId)
                .AsEnumerable());
            if (classIdsToDetach.Any())
            {
                foreach (var classId in classIdsToDetach)
                {
                    var detached = await _dbContext
                        .TeachersClasses
                        .FirstOrDefaultAsync(tc => tc.ClassId == classId);
                    _dbContext.Remove(detached);
                }
            }
            if (classIdsToPin.Any())
            {
                foreach (var classId in classIdsToPin)
                {
                    var toPin = new TeachersClasses
                    {
                        ClassId = classId,
                        TeacherId = teacherId
                    };
                    await _dbContext.AddAsync(toPin);
                }
            }
            await _dbContext.SaveChangesAsync();
        }
        public Task<Teacher> DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
