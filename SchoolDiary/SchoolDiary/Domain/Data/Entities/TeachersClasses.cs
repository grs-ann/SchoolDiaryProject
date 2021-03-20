using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Domain.Data.Entities
{
    public class TeachersClasses : BaseEntity
    {
        // Link to 'Teacher' table(FK).
        public Teacher Teacher { get; set; }
        public int TeacherId { get; set; }

        // Link to 'Class' table(FK).
        public Class Class { get; set; }
        public int ClassId { get; set; }
    }
}
