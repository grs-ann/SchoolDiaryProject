using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Domain.Models.Mark
{
    public class MarkToAddModel
    {
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public DateTime MarkTime { get; set; }
        public int SelectedMarkId { get; set; }
    }
}
