using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmAssistant.Models.ViewModels
{
    public class RoleViewModel
    {
        public AppRole Role { get; set; }
        public IQueryable<AppRole> RolesList { get; set; }
    }
}