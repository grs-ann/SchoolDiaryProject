using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Domain.Data.Entities
{
    [Keyless]
    public class Student
    {
        // Link to 'Classes' table.
        public Class Class { get; set; }
        public int ClassId { get; set; }


        // Link to 'Users' table.
        public virtual User User { get; set; }
        public int UserId { get; set; }
    }
}
