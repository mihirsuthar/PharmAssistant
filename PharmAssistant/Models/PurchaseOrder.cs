using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmAssistant.Models
{
    public class PurchaseOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int PurchaseOrderId { get; set; }
        [Required(ErrorMessage = "Purchase Date is required.")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Please enter amount.")]
        public double Amount { get; set; }
        [Required(ErrorMessage = "Please specify discount.")]
        public double Discount { get; set; }
        [Required(ErrorMessage = "Please specify tax.")]
        public double Tax { get; set; }
        [Required(ErrorMessage = "Grand total must not be empty.")]
        public double GrandTotal { get; set; }
        [StringLength(100)]
        [Column(TypeName = "char")]
        public string Remarks { get; set; }
        [StringLength(100)]
        [Column(TypeName = "char")]
        public string PaymentMode { get; set; }
        [Required]
        public bool PaymentStatus { get; set; }

        public int SupplierId { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}