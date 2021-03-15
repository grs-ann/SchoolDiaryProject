using Microsoft.EntityFrameworkCore;
using SchoolDiary.Domain.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Domain.Data
{
    public static class DBInitializer
    {
        public static void Initialize(DataContext _dbContext)
        {
            if ( _dbContext.Roles.Any())
            {
                 _dbContext.Roles.AddRange(
                    new Role
                    {
                        Id = 1,
                        Name = "admin",
                    },
                    new Role
                    {
                        Id = 2,
                        Name = "user"
                    });
            }
            if (_dbContext.Users.Any())
            {
                 _dbContext.Users.AddRange(
                    new User
                    {
                        Id = 1,
                        Login = "42ama",
                        Password = "123456",
                        RoleId = 1
                    });
            }
            _dbContext.SaveChangesAsync();
        }
    }
}
