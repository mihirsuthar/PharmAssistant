using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmAssistant.Models
{
    public class MedicineCategory
    {
        public MedicineCategory()
        {
            this.Suppliers = new HashSet<Supplier>();
            this.Medicines = new HashSet<Medicine>();
        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int CategoryId { get; set; }

        [StringLength(30)]
        [Column(TypeName = "varchar")]
        [Required(ErrorMessage = "Category Name is required.")]
        public string MedicineCategoryName { get; set; }

        public virtual ICollection<Medicine> Medicines { get; set; }
        public virtual ICollection<Supplier> Suppliers { get; set; }
    }
}