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
        public async Task RegisterAsync(RegisterModel model)
        {
            if (model != null)
            {
                await _dbContext.Users.AddAsync(
                    new User
                    {
                        Login = model.Login,
                        Password = _passwordHasher.GenerateHash(model.Password),
                        RoleId = model.RoleId
                    });
                await _dbContext.SaveChangesAsync();
            }
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
