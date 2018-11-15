using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmAssistant.Models
{
    public class PurchaseOrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]        
        [Column(Order =1)]        
        public string PurchaseOrderId { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "Please select medicine.")]
        [Column(Order = 2)]
        public int MedicineId { get; set; }

        [StringLength(30)]
        [Column(TypeName = "varchar")]        
        public string MedicineName { get; set; }

        public long BatchNumber { get; set; }

        [Required(ErrorMessage = "Please specify quantity.")]
        public int Quantity { get; set; }
                
        public double CostPrice { get; set; }
                
        public double SellingPrice { get; set; }

        public DateTime ExpiryDate { get; set; }

        public virtual PurchaseOrder PurchaseOrder { get; set; }
        public virtual Medicine Medicine { get; set; }
    }
}