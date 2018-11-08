using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmAsistant.Models
{
    public class StockEntry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int StockId { get; set; }

        [Required]
        public int ProductId { get; set; }
        [Required]
        public int PurchaseOrderId { get; set; }

        public virtual Product Product { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
    }
}