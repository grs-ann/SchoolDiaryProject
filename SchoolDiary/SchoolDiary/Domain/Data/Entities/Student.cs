using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Domain.Data.Entities
{
    public class Student
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Patronymic { get; set; }

        // Link to 'Classes' table.
        public Class Class { get; set; }
        public int ClassId { get; set; }


        // Link to 'Users' table.
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
