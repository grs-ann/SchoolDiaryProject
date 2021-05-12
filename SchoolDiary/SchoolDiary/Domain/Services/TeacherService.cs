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
    public interface ITeacherService
    {
        public IEnumerable<Class> GetPinnedClasses(int teacherId);
    }
    public class TeacherService : ITeacherService
    {
        private readonly DataContext _dbContext;
        public TeacherService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Class> GetPinnedClasses(int teacherId)
        {
            var pinnedClasses = _dbContext
                .Teachers
                .Include(t => t.Classes)
                .ThenInclude(t => t.Students)
                .ThenInclude(t => t.User)
                .FirstOrDefault(t => t.UserId == teacherId)
                .Classes;
            return pinnedClasses;
        }
    }
}
