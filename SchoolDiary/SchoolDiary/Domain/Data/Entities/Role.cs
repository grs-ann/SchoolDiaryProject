using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SchoolDiary.Domain.Data.Entities
{

    public class Role : BaseEntity
    {
        public string Name { get; set; }
        [JsonIgnore]
        public List<User> Users { get; set; } = new List<User>();
    }
}
