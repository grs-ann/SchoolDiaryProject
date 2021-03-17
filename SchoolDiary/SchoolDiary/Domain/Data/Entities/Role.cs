﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Domain.Data.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public List<User> Users { get; set; } = new List<User>();
    }
}
