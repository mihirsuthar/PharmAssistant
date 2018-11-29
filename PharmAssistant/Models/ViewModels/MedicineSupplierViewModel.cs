using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PharmAssistant.Models.ViewModels
{
    public class MedicineSupplierViewModel
    {
        [Key]
        public int CategoryId { get; set; }
        public int MedicineId { get; set; }
        public List<int> SelectedSuppliersForCategory { get; set; }
        public List<int> SelectedSuppliersForMedicine { get; set; }

        //public ICollection<MedicineCategory> MedicineCategories { get; set; }
        //public ICollection<Medicine> Medicines { get; set; }
        //public ICollection<Supplier> Suppliers { get; set; }
        //public List<int> SelectedSuppliersForCategory { get; set; }
        //public List<int> SelectedSuppliersForMedicine { get; set; }
    }
}