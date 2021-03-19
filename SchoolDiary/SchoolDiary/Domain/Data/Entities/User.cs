using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Domain.Data.Entities
{
    public class User : BaseEntity
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Patronymic { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public int RoleId { get; set; }
        // User role.
        public Role Role{ get; set; }

        public Student Student { get; set; }
        public Teacher Teacher { get; set; }
    }
}
