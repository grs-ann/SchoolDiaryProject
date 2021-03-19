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
    /// <summary>
    /// This interface represents operations
    /// for work with database 'Classes' table.
    /// </summary>
    public interface IClassService : ICRUD<Class>
    {
        Task<Class> AddNewClassAsync(string name);
        Task<Class> EditClassAsync(ClassModel model);
    }
    /// <summary>
    /// This service contains a set of methods 
    /// with logic for managing student classes.
    /// </summary>
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
        public async Task<Class> GetByIdAsync(int id)
        {
            var _class = await _dbContext.Classes
                .FirstOrDefaultAsync(c => c.Id == id);
            return _class;
        }
        public async Task<Class> AddNewClassAsync(string name)
        {
            var existingClass = _dbContext.Classes
                .FirstOrDefaultAsync(c => c.Name == name);
            if (existingClass == null)
            {
                var newClass = new Class
                {
                    Name = name
                };
                await _dbContext.AddAsync(newClass);
                await _dbContext.SaveChangesAsync();
                return newClass;
            }
            return null;
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
        public async Task<Class> DeleteByIdAsync(int id)
        {
            var classToDelete = await _dbContext.Classes
                .FirstOrDefaultAsync(c => c.Id == id);
            if (classToDelete != null)
            {
                _dbContext.Remove(classToDelete);
                return classToDelete;
            }
            return null;
        }
    }
}
