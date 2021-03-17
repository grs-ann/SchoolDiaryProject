using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Helpers.Interfaces
{
    public interface IPasswordHasher : IDisposable
    {
        string GenerateHash(string password);
        bool IsPasswordMatchingHash(string password, string savedPasswordHash);
    }
}
