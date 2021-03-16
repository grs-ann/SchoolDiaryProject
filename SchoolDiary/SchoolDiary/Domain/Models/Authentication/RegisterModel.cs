using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Domain.Models.Authentication
{
    public class RegisterModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}
