using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Domain.Data.Entities
{
    public class Grade : BaseEntity
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int MarkId { get; set; }
        public Mark Mark { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public DateTime GradeDate { get; set; }
    }
}
