using Microsoft.EntityFrameworkCore;
using SchoolDiary.Domain.Data;
using SchoolDiary.Domain.Data.Entities;
using SchoolDiary.Domain.Models.Mark;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolDiary.Domain.Services
{
    public interface IMarkService
    {
        IEnumerable<Grade> GetConcreteStudentMarksBySubjectId(int studentId, int subjectId, DateTime firstDate, DateTime lastDate);
        IEnumerable<Mark> GetAllMarks();
        Grade ChangeMark(MarkToChangeModel model);
        Grade AddNewMark(MarkToAddModel model);
    }
    public class MarkService : IMarkService
    {
        private readonly DataContext _dbContext;
        public MarkService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// Adds a new mark to concrete student
        /// with concrete subject.
        /// </summary>
        /// <param name="model">Contains mark datetime,
        /// studentId, subjectId, markId.</param>
        /// <returns></returns>
        public Grade AddNewMark(MarkToAddModel model)
        {
            var gradeToAdd = new Grade
            {
                GradeDate = model.MarkTime,
                MarkId = model.SelectedMarkId,
                StudentId = model.StudentId,
                SubjectId = model.SubjectId
            };
            
            if (gradeToAdd != null)
            {
                _dbContext.Add(gradeToAdd);
                _dbContext.SaveChanges();
                return gradeToAdd;
            }
            return null;
        }

        /// <summary>
        /// Changes concrete student mark.
        /// </summary>
        /// <returns></returns>
        public Grade ChangeMark(MarkToChangeModel model)
        {
            var gradeToChange = _dbContext
                .Marks
                .Include(m => m.Grades)
                .SelectMany(m => m.Grades)
                .FirstOrDefault(g => g.Id == model.ConcreteMarkId);
            
            if (gradeToChange != null)
            {
                var selectedMark = _dbContext
                    .Marks
                    .FirstOrDefault(m => m.Id == model.SelectedMarkId);
                if (selectedMark != null)
                {
                    gradeToChange.Mark = selectedMark;
                    gradeToChange.GradeDate = model.DateTime;
                    _dbContext.SaveChanges();
                    return gradeToChange;
                }
            }
            return null;
        }

        public IEnumerable<Mark> GetAllMarks()
        {
            var marks = _dbContext.Marks;
            return marks;
        }

        /// <summary>
        /// Gets collection of student
        /// marks by student Id and subject Id.
        /// </summary>
        /// <param name="model">Contains studentId and subjectId</param>
        /// <returns>Collection of marks.</returns>
        public IEnumerable<Grade> GetConcreteStudentMarksBySubjectId(int studentId, int subjectId, DateTime firstDate, DateTime lastDate)
        {
            var student = _dbContext
                .Students
                .Include(s => s.Grades)
                .Include(s => s.Marks)
                .FirstOrDefault(x => x.Id == studentId);
            var marks = student
                .Grades.Where(g => g.SubjectId == subjectId && (g.GradeDate >= firstDate && g.GradeDate <= lastDate));
            return marks;
        }
    }
}
