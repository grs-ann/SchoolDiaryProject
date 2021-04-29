using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Domain.Models.Student
{
    /// <summary>
    /// Represents student model state,
    /// this data is expected to come from 
    /// frontend.
    /// </summary>
    public class EditStudentModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Минимальная длина имени - 2 символа!")]
        public string Firstname { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Минимальная длина фамилии - 2 символа!")]
        public string Lastname { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Минимальная длина отчества - 2 символа!")]
        public string Patronymic { get; set; }
        [Required]
        [MinLength(4, ErrorMessage = "Длина для логина не меньше 4 символов!")]
        public string Login { get; set; }
        public string Phone { get; set; }
        [Required(ErrorMessage = "Необходимо указать класс для ученика!")]
        public int ClassId { get; set; }
    }
}
