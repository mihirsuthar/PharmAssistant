using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PharmAssistant.Models.ViewModels
{
    public class MedicineCategoryViewModel
    {
        [Key]
        public MedicineCategory Category { get; set; }
        public ICollection<MedicineCategory> CategoryList { get; set; }
    }
}