using Microsoft.EntityFrameworkCore;
using SchoolDiary.Domain.Data;
using SchoolDiary.Domain.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolDiary.Domain.Services
{
    public interface IMarkService
    {
        IEnumerable<Mark> GetConcreteStudentMarksBySubjectId(int studentId, int subjectId, DateTime firstDate, DateTime lastDate);
    }
    public class MarkService : IMarkService
    {
        private readonly DataContext _dbContext;
        public MarkService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// Gets collection of student
        /// marks by student Id and subject Id.
        /// </summary>
        /// <param name="model">Contains studentId and subjectId</param>
        /// <returns>Collection of marks.</returns>
        public IEnumerable<Mark> GetConcreteStudentMarksBySubjectId(int studentId, int subjectId, DateTime firstDate, DateTime lastDate)
        {
            var student = _dbContext
                .Students
                .Include(s => s.Grades)
                .ThenInclude(s => s.Mark)
                .FirstOrDefault(s => s.Id == studentId);
            var studentMarks = student.Marks.Where(x => x.Grades.Where(x => x.SubjectId == subjectId).Any());
            return studentMarks;
        }
    }
}
