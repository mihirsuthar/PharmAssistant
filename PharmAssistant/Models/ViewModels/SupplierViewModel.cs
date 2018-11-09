using PharmAssistant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmAssistant.Models.ViewModels
{
    public class SupplierViewModel
    {
        public Supplier Supplier { get; set; }
        public ICollection<Supplier> SuppliersList { get; set; }
    }
}