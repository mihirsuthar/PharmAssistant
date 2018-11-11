using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmAssistant.Models.ViewModels
{
    public class ManufacturerViewModel
    {
        public Manufacturer Manufacturer { get; set; }
        public ICollection<Manufacturer> ManufacturersList { get; set; }

    }
}