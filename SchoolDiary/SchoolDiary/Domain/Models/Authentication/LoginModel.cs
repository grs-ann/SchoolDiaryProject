﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Domain.Models.Authentication
{
    public class LoginModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}