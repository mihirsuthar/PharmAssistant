using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmAsistant.Models
{
    public class PurchaseOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int PurchaseOrderId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public double Discount { get; set; }
        [Required]
        public double Tax { get; set; }
        [Required]
        public double GrandTotal { get; set; }
        [StringLength(100)]
        [Column(TypeName = "char")]
        public string Remarks { get; set; }
        [Required]
        public bool Payment { get; set; }

        public int SupplierId { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}