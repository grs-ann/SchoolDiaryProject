using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SchoolDiary.Domain.Data.Entities
{
    public class User : BaseEntity
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Patronymic { get; set; }
        public string Login { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public string Phone { get; set; }

        // User token.
        public string Token { get; set; }

        public int RoleId { get; set; }
        // User role.
        public Role Role{ get; set; }
        [JsonIgnore]
        public Student Student { get; set; }
        [JsonIgnore]
        public Teacher Teacher { get; set; }
    }
}
