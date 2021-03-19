using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Domain.Models.Authentication
{
    public class RegisterStudentModel : BaseRegisterModel
    {
        // ClassId in 'Classes' table.
        public int ClassId { get; set; }
    }
}
