using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmAssistant.Models
{
    public class Supplier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Supplier Id is required")]
        public int SupplierId { get; set; }

        [StringLength(25)]
        [Column(TypeName = "char")]
        [Required(ErrorMessage = "Supplier name is required")]
        public string Name { get; set; }

        [StringLength(50)]
        [Column(TypeName = "char")]
        [Required(ErrorMessage = "Supplier address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Contact number is required")]
        public long ContactNo { get; set; }

        [StringLength(30)]
        [Column(TypeName = "char")]
        [Required(ErrorMessage = "Email Id is required")]
        public string EmailId { get; set; }

        [StringLength(40)]
        [Column(TypeName = "char")]
        public string Description { get; set; }

        public virtual ICollection<Medicine> Products { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
    }
}