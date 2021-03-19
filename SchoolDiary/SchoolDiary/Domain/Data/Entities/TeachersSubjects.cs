using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Domain.Data.Entities
{
    public class TeachersSubjects : BaseEntity
    {
        // Link to 'Classes' table.
        public Teacher Teacher { get; set; }
        public int TeacherId { get; set; }

        // Link to 'Users' table.
        public Subject Subject { get; set; }
        public int SubjectId { get; set; }
    }
}
