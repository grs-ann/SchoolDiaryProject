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
        // Secret cookie name for jwt token.
        //private const string CookieKey = "Bearer";
        // todo: remove unnecessary logic from the controller here.
        public string Login(LoginModel model)
        {
            if (model != null)
            {
                var identity = GetIdentity(model);
                if (identity != null)
                {
                    var jwt = CreateJWTToken(identity);
                    // Save the jwt-token to browser cookies.
                    //_httpContextAccessor.HttpContext.Response.Cookies.Append(CookieKey, jwt);
                    return jwt;
                }
            }
            return null;
        }
        public void Register(RegisterModel model)
        {
            if (model != null)
            {
                
                var userRole = _dbContext.Roles.FirstOrDefault(r => r.Id == model.RoleId);
                _dbContext.Users.Add(
                    new User
                    {
                        Login = model.Login,
                        Password = _passwordHasher.GenerateHash(model.Password),
                        Role = userRole
                    });
                _dbContext.SaveChanges();
            }
        }
        private string CreateJWTToken(ClaimsIdentity identity)
        {
            var utcNow = DateTime.UtcNow;
            // Create a JWT-token.
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: utcNow,
                    claims: identity.Claims,
                    expires: utcNow.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }
        private ClaimsIdentity GetIdentity(LoginModel model)
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
