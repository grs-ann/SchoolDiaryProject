using System.Collections.Generic;

namespace SchoolDiary.Domain.Data.Entities
{
    public class Teacher : BaseEntity
    {   
        public decimal Salary { get; set; }
        // Link to 'Users' table.
        public User User { get; set; }
        public int UserId { get; set; }

        public List<Class> Classes{ get; set; } = new List<Class>();
        public List<Subject> Subjects { get; set; } = new List<Subject>();
    }
}
