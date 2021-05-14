using System.Collections.Generic;

namespace SchoolDiary.Domain.Data.Entities
{
    public class Student : BaseEntity
    {
        // Link to 'Classes' table.
        public Class Class { get; set; }
        public int ClassId { get; set; }

        // Link to 'Users' table.
        public User User { get; set; }
        public int UserId { get; set; }

        public List<Mark> Marks { get; set; } = new List<Mark>();
        public List<Grade> Grades { get; set; } = new List<Grade>();
    }
}
