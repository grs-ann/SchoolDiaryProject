using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SchoolDiary.Domain.Data;
using SchoolDiary.Domain.Data.Entities;
using SchoolDiary.Domain.Models.Authentication;
using SchoolDiary.Domain.Services.Interfaces;
using SchoolDiary.Helpers;
using SchoolDiary.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDiary.Domain.Services
{
    /// <summary>
    /// This interface represents
    /// opeartions with accounts.
    /// </summary>
    public interface IAccountService
    {
        User Authenticate(string login, string password);
        Task<User> RegisterStudentAsync(RegisterStudentModel model);
        Task<User> RegisterTeacherAsync(RegisterTeacherModel model);
        Task<User> DeleteUserAsync(int id);
    }
    /// <summary>
    /// This service contains a set of methods 
    /// with logic for managing accounts and authenticate.
    /// </summary>
    public class AccountService : IAccountService
    {
        private readonly DataContext _dbContext;
        private readonly IPasswordHasher _passwordHasher;
        private readonly AppSettings _appSettings;
        public AccountService(DataContext dbContext,
            IPasswordHasher passwordHasher,
            IOptions<AppSettings> appSettings)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _appSettings = appSettings.Value;
        }
        public User Authenticate(string login, string password)
        {
            var user = _dbContext.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Login == login);
            if (user != null && _passwordHasher.IsPasswordMatchingHash(password, user.Password))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.Name)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);
                return user.WithoutPassword();
            }
            return null;
        }

        public async Task<User> DeleteUserAsync(int id)
        {
            var userToDelete = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == id);
            _dbContext.Users.Remove(userToDelete);
            await _dbContext.SaveChangesAsync();
            return userToDelete;
        }

        /// <summary>
        /// Register a new student.
        /// </summary>
        /// <param name="model">Register Student Model.</param>
        /// <returns>Registered user.</returns>
        public async Task<User> RegisterStudentAsync(RegisterStudentModel model)
        {
            if (model != null)
            {
                var newUser = BaseRegister(model);
                await _dbContext.Users.AddAsync(newUser);
                await _dbContext.Students.AddAsync(
                    new Student
                    {
                        User = newUser,
                        ClassId = model.ClassId,
                    });
                await _dbContext.SaveChangesAsync();
                return newUser;
            }
            return null;
        }
        /// <summary>
        /// Register a new teacher.
        /// </summary>
        /// <param name="model">Register Teacher Model.</param>
        /// <returns>Registered user.</returns>
        public async Task<User> RegisterTeacherAsync(RegisterTeacherModel model)
        {
            if (model != null)
            {
                var newUser = BaseRegister(model);
                await _dbContext.Users.AddAsync(newUser);
                await _dbContext.Teachers.AddAsync(
                    new Teacher
                    {
                        User = newUser,
                        Salary = model.Salary
                    });
                await _dbContext.SaveChangesAsync();
                return newUser;
            }
            return null;
        }
        /// <summary>
        /// Register a new 'base' user(not teacher or student).
        /// </summary>
        /// <param name="model">Base Registe rModel</param>
        /// <returns>Registered user.</returns>
        private User BaseRegister(BaseRegisterModel model)
        {
            var user = new User
            {
                Login = model.Login,
                Password = _passwordHasher.GenerateHash(model.Password),
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Patronymic = model.Patronymic,
                Phone = model.Phone,
                RoleId = model.RoleId
            };
            return user;
        }
    }
}
