using SchoolDiary.Domain.Data;
using SchoolDiary.Domain.Data.Entities;
using SchoolDiary.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Domain.Services
{
    public class UserService<T> : IBaseCRUDOperations<T> where T : BaseEntity
    {
        private readonly DataContext _dbContext;
        public UserService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<T> GetAll()
        {
            var result = _dbContext.Set<T>().AsEnumerable();
            return result;
        }

        public T GetById(int id)
        {
            var result = _dbContext.Set<T>().FirstOrDefault(x => x.Id == id);
            return result;
        }
    }
}
