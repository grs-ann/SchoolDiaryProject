using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Domain.Data.Entities
{
    public class LessonTime : BaseEntity 
    {
        // Link to 'Time' table(FK). 
        public Time Time { get; set; }
        public int TimeId { get; set; }
    }
}
