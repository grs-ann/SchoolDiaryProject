using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
using System.Threading.Tasks;

namespace SchoolDiary.Domain.Services
{
    public interface IAccountService
    {
        string Authenticate(LoginModel model);
        Task<User> RegisterStudentAsync(RegisterStudentModel model);
        Task<User> RegisterTeacherAsync(RegisterTeacherModel model);
        void Unauthenticate();
    }
    public class AccountService : IAccountService
    {
        private readonly DataContext _dbContext;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccountService(DataContext dbContext, IPasswordHasher passwordHasher,
            IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _httpContextAccessor = httpContextAccessor;
        }
        public string Authenticate(LoginModel model)
        {
            if (model != null)
            {
                var claims = GetClaims(model);
                if (claims != null)
                {
                    var jwt = CreateJWTToken(claims);
                    // Save the jwt-token to browser cookies.
                    // .AspNetCore.Application.Id
                    _httpContextAccessor.HttpContext.Response.Cookies.Append("refregeratorprice", jwt,
                        new CookieOptions
                        {
                            MaxAge = TimeSpan.FromMinutes(60)
                        });
                    return jwt;
                }
            }
            return null;
        }
        public void Unauthenticate()
        {
            if (_httpContextAccessor.HttpContext.Request.Cookies.ContainsKey("refregeratorprice"))
            {
                _httpContextAccessor.HttpContext.Response.Cookies.Delete("refregeratorprice");
            }
        }
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
        private string CreateJWTToken(ClaimsIdentity claims)
        {
            // Create a JWT-token.
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: DateTime.Now,
                    claims: claims.Claims,
                    expires: DateTime.Now.Add(TimeSpan.FromSeconds(10)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }
        private ClaimsIdentity GetClaims(LoginModel model)
        {
            var user = _dbContext.Users
                .Include(r => r.Role)
                .FirstOrDefault(x => x.Login == model.Login);
            if (user != null)
            {
                if (_passwordHasher.IsPasswordMatchingHash(model.Password, user.Password))
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name),
                };
                    var claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                    return claimsIdentity;
                }
            }
            return null;
        }
    }
}
