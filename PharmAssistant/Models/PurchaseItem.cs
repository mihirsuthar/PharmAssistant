using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmAssistant.Models
{
    public class PurchaseItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Column(Order =1)]
        public int PurchaseOrderId { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "Please select medicine.")]
        [Column(Order = 2)]
        public int MedicineId { get; set; }
        
        public long BatchNumber { get; set; }

        [Required(ErrorMessage = "Please specify quantity.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Cost price is required.")]
        public double CostPrice { get; set; }

        [Required(ErrorMessage = "Selling price is required.")]
        public double SellingPrice { get; set; }

        public DateTime ExpiryDate { get; set; }

        public virtual PurchaseOrder PurchaseOrder { get; set; }
        public virtual Medicine Medicine { get; set; }
    }
}