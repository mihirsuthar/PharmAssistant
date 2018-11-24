using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmAssistant.Models.ViewModels
{
    public class MedicineViewModel
    {
        public Medicine Medicine { get; set; }
        public int SupplierId { get; set; }
        //public int ManufacturerId { get; set; }
        //public int CategoryId { get; set; }
        //public int ShelfId { get; set; }
    }
}