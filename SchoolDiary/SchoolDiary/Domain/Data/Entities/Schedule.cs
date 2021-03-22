using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Domain.Data.Entities
{
    public class Schedule : BaseEntity
    {
        // Link to 'Days' table(FK).
        public Day Day { get; set; }
        public int DayId { get; set; }
        // Link to 'Classes' table(FK).
        public Class Class{ get; set; }
        public int ClassId { get; set; }
        // Link to 'Subjects' table(FK).
        public Subject Subject { get; set; }
        public int SubjectId { get; set; }
        // Link to 'Teachers' table(FK).
        public Teacher Teacher{ get; set; }
        public int TeacherId { get; set; }
        // Link to 'LessonTimes' table(FK).
        public LessonTime LessonTime { get; set; }
        public int LessonTimeId { get; set; }
    }
}
