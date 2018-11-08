using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmAsistant.Models
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
        [Required]
        [Column(Order = 2)]
        public int ProductId { get; set; }
        [Required]
        public long BatchNo { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double CostPrice { get; set; }
        [Required]
        public double SellingPrice { get; set; }
        [Required]
        public DateTime ExpiryDate { get; set; }

        public virtual PurchaseOrder PurchaseOrder { get; set; }
        public virtual Product Product { get; set; }
    }
}