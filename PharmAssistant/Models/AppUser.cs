﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmAsistant.Models
{
    public class AppUser: IdentityUser
    {
        //additional properties go here
        public string City { get; set; }
    }
}