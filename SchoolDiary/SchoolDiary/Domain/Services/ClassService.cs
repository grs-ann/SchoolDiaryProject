using Microsoft.EntityFrameworkCore;
using SchoolDiary.Domain.Data;
using SchoolDiary.Domain.Data.Entities;
using SchoolDiary.Domain.Models.Class;
using SchoolDiary.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Domain.Services
{
    public interface IClassService : ICRUD<Class>
    {
        Task AddNewClassAsync(ClassModel model);
        Task<Class> EditClassAsync(ClassModel model);
    }
    public class ClassService : IClassService
    {
        private readonly DataContext _dbContext;
        public ClassService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Class> GetAll()
        {
            var allClasses = _dbContext.Classes;
            return allClasses;
        }

        public async Task<Class> GetById(int id)
        {
            var _class = await _dbContext.Classes
                .FirstOrDefaultAsync(c => c.Id == id);
            return _class;
        }
        public async Task AddNewClassAsync(ClassModel model)
        {
            // todo: unique name!!
            var newClass = new Class
            {
                Name = model.Name
            };
            await _dbContext.AddAsync(newClass);
            await _dbContext.SaveChangesAsync(); 
        }
        public async Task<Class> EditClassAsync(ClassModel model)
        {
            var editedClass = await _dbContext.Classes
                .FirstOrDefaultAsync(c => c.Id == c.Id);
            if (editedClass != null)
            {
                editedClass.Name = model.Name;
                return editedClass;
            }
            return null;
        }
    }
}
