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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(20)]
        [Column(TypeName = "varchar")]
        public string PurchaseOrderId { get; set; }

        [StringLength(32)]
        [Column(TypeName = "varchar")]
        public string PurchaseOrderCode { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? PurchaseDate { get; set; }

        [Required(ErrorMessage = "Please enter amount.")]
        public double OrderCost { get; set; }

        [Required(ErrorMessage = "Please specify discount.")]
        public double Discount { get; set; }

        [Required(ErrorMessage = "Please specify tax.")]
        public double Tax { get; set; }

        [Required(ErrorMessage = "Grand total must not be empty.")]
        public double AmountPaid { get; set; }

        [StringLength(100)]
        [Column(TypeName = "char")]
        public string Remarks { get; set; }

        [StringLength(25)]
        [Column(TypeName = "char")]
        public string PaymentMode { get; set; }

        [Required]
        public bool PaymentStatus { get; set; }

        public bool OrderStatus { get; set; }

        public int SupplierId { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}