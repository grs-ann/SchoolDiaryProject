using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Domain.Models.Authentication
{
    public class RegisterStudentModel : BaseRegisterModel
    {
        // ClassId in 'Classes' table.
        [Required(ErrorMessage = "Необходимо указать класс для ученика!")]
        public int ClassId { get; set; }
    }
}
