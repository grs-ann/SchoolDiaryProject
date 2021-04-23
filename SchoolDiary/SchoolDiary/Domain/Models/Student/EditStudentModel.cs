using System;
using System.Collections.Generic;
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
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Patronymic { get; set; }
        public string Login { get; set; }
        public string Phone { get; set; }
        public int ClassId { get; set; }
    }
}
