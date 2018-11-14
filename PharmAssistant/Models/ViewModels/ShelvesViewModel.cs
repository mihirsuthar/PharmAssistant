using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmAssistant.Models.ViewModels
{
    public class ShelvesViewModel
    {
        public Shelf Shelf { get; set; }
        public ICollection<Shelf> ShelfList { get; set; }
    }
}