using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Domain.Models.Authentication
{
    public class RegisterTeacherModel : BaseRegisterModel
    {
        [Required(ErrorMessage = "Необходимо указать зарплату учителя.")]
        [Range(0, 300000, ErrorMessage = "Зарплата должна быть в пределах от 0 до 300000 рублей.")]
        public decimal Salary { get; set; }
    }
}
