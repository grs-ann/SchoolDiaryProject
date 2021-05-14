using System.Collections.Generic;

namespace SchoolDiary.Domain.Data.Entities
{
    public class Subject : BaseEntity
    {
        public string Title { get; set; }
        public List<Teacher> Teachers { get; set; } = new List<Teacher>();
    }
}
