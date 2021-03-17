using SchoolDiary.Domain.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Domain.Services.Interfaces
{
    public interface IBaseCRUDOperations<T> where T: BaseEntity
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
    }
}
