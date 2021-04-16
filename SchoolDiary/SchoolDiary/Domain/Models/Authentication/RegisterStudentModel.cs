using System.ComponentModel.DataAnnotations;

namespace SchoolDiary.Domain.Models.Authentication
{
    public class RegisterStudentModel : BaseRegisterModel
    {
        // ClassId in 'Classes' table.
        [Required(ErrorMessage = "Необходимо указать класс для ученика!")]
        public int ClassId { get; set; }
    }
}
