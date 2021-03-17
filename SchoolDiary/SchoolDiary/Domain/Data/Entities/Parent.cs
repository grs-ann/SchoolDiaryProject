using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Domain.Data.Entities
{
    public class Parent
    {
        // Link to 'Users' table.
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
