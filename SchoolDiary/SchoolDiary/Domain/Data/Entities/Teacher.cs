using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Domain.Data.Entities
{
    public class Teacher : BaseEntity
    {   
        public decimal Salary { get; set; }
        // Link to 'Users' table.
        public User User { get; set; }
        public int UserId { get; set; }

        public List<Class> Classes{ get; set; } = new List<Class>();
    }
}
