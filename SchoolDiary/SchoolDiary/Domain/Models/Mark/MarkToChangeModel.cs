using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Domain.Models.Mark
{
    public class MarkToChangeModel
    {
        public int ConcreteMarkId { get; set; }
        public DateTime DateTime { get; set; }
        public int SelectedMarkId { get; set; }
    }
}
