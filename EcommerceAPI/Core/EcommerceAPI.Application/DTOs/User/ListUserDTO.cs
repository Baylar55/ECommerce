﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.DTOs.User
{
    public class ListUserDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string NameSurname { get; set; }
        public string Username { get; set; }
        public bool TwoFactorEnabled { get; set; }
    }
}