using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Domain.Services.Interfaces
{
    /// <summary>
    /// This interface represents base CRUD
    /// opeartions with <T> entities.
    /// </summary>
    /// <typeparam name="T">Entity</typeparam>
    public interface ICRUD<T>
    {
        IEnumerable<T> GetAll();
        Task<T> GetByIdAsync(int id);
        Task<T> DeleteByIdAsync(int id);
    }
}
