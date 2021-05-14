using Microsoft.EntityFrameworkCore;
using SchoolDiary.Domain.Data;
using SchoolDiary.Domain.Data.Entities;
using SchoolDiary.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Domain.Services
{
    public interface ISubjectService : ICRUD<Subject>
    {
        IEnumerable<Subject> GetPinnedSubjectsByTeacherId(int teacherId);
    }
    public class SubjectService : ISubjectService
    {
        private readonly DataContext _dbContext;
        public SubjectService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// Gets all subjects from
        /// database table.
        /// </summary>
        /// <returns>Collection of subjects.</returns>
        public IEnumerable<Subject> GetAll()
        {
            var subjects = _dbContext.Subjects;
            return subjects;
        }
        /// <summary>
        /// Gets subjects pinned to 
        /// concrete teacher.
        /// </summary>
        /// <param name="teacherId">Teacher Id in databse 'Teachers' table.</param>
        /// <returns>Collection of subjects</returns>
        public IEnumerable<Subject> GetPinnedSubjectsByTeacherId(int teacherId)
        {
            var teacherSubjects = _dbContext
                .Teachers
                .Include(t => t.Subjects)
                .FirstOrDefault(t => t.UserId == teacherId)
                .Subjects;
            return teacherSubjects;
        }

        public Task<Subject> DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task<Subject> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
