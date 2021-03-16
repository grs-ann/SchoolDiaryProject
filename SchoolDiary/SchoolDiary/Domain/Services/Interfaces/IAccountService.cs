using SchoolDiary.Domain.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Domain.Services.Interfaces
{
    public interface IAccountService
    {
        string Login(LoginModel model);
        void Register(RegisterModel model);
    }
}
