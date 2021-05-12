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
        IEnumerable<Class> GetPinnedClassesByTeacherId(int teacherId);
        Task<Teacher> ChangeTeacherAsync(EditTeacherModel model);
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
        public IEnumerable<Class> GetPinnedClassesByTeacherId(int id)
        {
            var pinnedClasses = _dbContext.Teachers
                .Include(t => t.Classes)
                .FirstOrDefault(t => t.UserId == id)
                .Classes;
            return pinnedClasses;
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
                        await ChangePinnedClassesForTeacherAsync(teacherId, model.ClassIds);
                    }
                    await _dbContext.SaveChangesAsync();
                    return teacherToChange;
                }
            }
            return null;
        }
        public Task<Teacher> DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Changes the bound classes to the teacher
        /// by his id.
        /// </summary>
        /// <param name="teacherId">Teacher id in database.</param>
        /// <param name="classIds">Id's collection, comes from client.</param>
        /// <returns></returns>
        private async Task ChangePinnedClassesForTeacherAsync(int teacherId, List<int> classIds)
        {
            // The concrete teacher with specified Id.
            var teacher = await _dbContext
                .Teachers
                .Include(t => t.Classes)
                .FirstOrDefaultAsync(x => x.Id == teacherId);
            // All classes assigned to the teacher initially.
            var alreadyPinnedClasses = _dbContext.Teachers
                .Include(t => t.Classes)
                .FirstOrDefault(t => t.Id == teacherId)
                .Classes
                .ToList();
            if (alreadyPinnedClasses.Any())
            {
                if (classIds.Any())
                {
                    // These classes are not contained in
                    // the received data and must be removed. 
                    var classesToDetach = alreadyPinnedClasses.Where(x => classIds.Where(y => y != x.Id).Any()).ToList();
                    if (classesToDetach.Any())
                    {
                        foreach (var cl in classesToDetach)
                        {
                            teacher.Classes.Remove(cl);
                        }
                    }
                    // These classes need to be assigned to the teacher.
                    var classesToPin = _dbContext
                            .Classes
                            .Include(c => c.Teachers)
                            .Where(c => classIds.Any(ci => c.Id == ci))
                            .ToList();
                    foreach (var item in classesToPin)
                    {
                        teacher.Classes.Add(item);
                    }
                }
                // If the ID of the classes did not come
                // from the front at all(their number is 0),
                // then we unpin all the assigned classes.
                else
                {
                    foreach (var cl in alreadyPinnedClasses)
                    {
                        teacher.Classes.Remove(cl);
                    }
                }
            }
            // If not a single class is assigned to the teacher,
            // then we simply attach all those who have come. 
            else
            {
                var classesToPin = _dbContext
                    .Classes
                    .Include(c => c.Teachers)
                    .Where(c => classIds.Any(ci => c.Id == ci))
                    .ToList();
                foreach (var cl in classesToPin)
                {
                    teacher.Classes.Add(cl);
                }
            }
        }
    }
}
