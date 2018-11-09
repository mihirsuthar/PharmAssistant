using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmAssistant.Models
{
    public class Medicine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int MedicineId { get; set; }

        [StringLength(30)]
        [Column(TypeName = "char")]
        [Required(ErrorMessage = "Medicine name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please select manufacturer.")]
        public int ManufacturerId { get; set; }

        [Required(ErrorMessage = "Please select medicine category.")]
        public int CategoryId { get; set; }

        public virtual ICollection<Supplier> Suppliers { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        public virtual MedicineCategory MedicineCategory { get; set; }
    }
}