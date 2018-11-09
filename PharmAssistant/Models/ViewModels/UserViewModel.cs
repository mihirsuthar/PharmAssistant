using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmAssistant.Models.ViewModels
{
    public class UserViewModel
    {
        public CreateUserModel User { get; set; }

        public IQueryable<AppUser> UsersList { get; set; }
    }
}