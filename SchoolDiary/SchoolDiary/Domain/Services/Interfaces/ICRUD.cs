using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Domain.Services.Interfaces
{
    public interface ICRUD<T>
    {
        IEnumerable<T> GetAll();
        Task<T> GetById(int id);
    }
}
