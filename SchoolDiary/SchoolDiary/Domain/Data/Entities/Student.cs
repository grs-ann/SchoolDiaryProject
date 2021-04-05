using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
    }
}
