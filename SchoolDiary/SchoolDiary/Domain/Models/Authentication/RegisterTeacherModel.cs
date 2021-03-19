using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Domain.Models.Authentication
{
    public class RegisterTeacherModel : BaseRegisterModel
    {
        public decimal Salary { get; set; }
    }
}
