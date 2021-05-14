using System.Collections.Generic;

namespace SchoolDiary.Domain.Data.Entities
{
    public class Mark : BaseEntity
    {
        public string Name { get; set; }
        public List<Student> Students { get; set; } = new List<Student>();
        public List<Grade> Grades { get; set; } = new List<Grade>();
    }
}
