using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Domain.Models.Schedule
{
    public class ScheduleModel
    {
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
        public int DayId { get; set; }
        public int TeacherId { get; set; }
        public int LessonTimeId { get; set; }
    }
}
