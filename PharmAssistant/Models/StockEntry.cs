using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmAssistant.Models
{
    public class StockEntry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int StockId { get; set; }

        [Required(ErrorMessage = "Please select medicine.")]
        public int MedicineId { get; set; }
        [Required(ErrorMessage = "Purchase order id is required.")]
        public int PurchaseOrderId { get; set; }

        public virtual Medicine Medicine { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
    }
}