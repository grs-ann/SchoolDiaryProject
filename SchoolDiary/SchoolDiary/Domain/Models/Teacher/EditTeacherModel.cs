using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolDiary.Domain.Models.Teacher
{
    public class EditTeacherModel
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
        [Required(ErrorMessage = "Необходимо указать зарплату учителя.")]
        [Range(0, 300000, ErrorMessage = "Зарплата должна быть в пределах от 0 до 300000 рублей.")]
        public decimal Salary { get; set; }
        // List of classIds, pinned to the teacher.
        public List<int> ClassIds { get; set; }
        // List of SubjectIds, pinned to the teacher.
        public List<int> SubjectIds { get; set; }
    }
}
