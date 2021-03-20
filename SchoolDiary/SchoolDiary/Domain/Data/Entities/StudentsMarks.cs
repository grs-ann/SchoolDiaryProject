using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Domain.Data.Entities
{
    public class StudentsMarks : BaseEntity
    {
        public DateTime DateTime { get; set; }

        // Link to 'Marks' table(FK).
        public Mark Mark { get; set; }
        public int MarkId { get; set; }

        // Link to 'Students' table(FK).
        public Student Student { get; set; }
        public int StudentId { get; set; }

        // Link to 'Subjects"' table(FK).
        public Subject Subject { get; set; }
        public int SubjectId { get; set; }
    }
}
