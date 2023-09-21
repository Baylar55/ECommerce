﻿using EcommerceAPI.Domain.Entities.Base;
using EcommerceAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Domain.Entities
{
    public class Endpoint : BaseEntity
    {
        public string ActionType { get; set; }
        public string HttpType { get; set; }
        public string Definition { get; set; }
        public string Code { get; set; }
        public Menu Menu { get; set; }
        public ICollection<AppRole> AppRoles { get; set; }
        public Endpoint()
        {
            AppRoles = new HashSet<AppRole>();
        }
    }
}
