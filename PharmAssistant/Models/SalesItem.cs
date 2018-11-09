using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmAssistant.Models
{
    public class SalesItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "Sales Order Id is required.")]
        [Column(Order = 1)]
        public int SalesOrderId { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "Medicine Id is required.")]
        [Column(Order = 2)]
        public int MedicineId { get; set; }
        
        [Required(ErrorMessage = "Batch Number is required.")]
        public long BatchNumber { get; set; }

        [Required(ErrorMessage = "Please enter selling price.")]
        public float SellingPrice { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Order item total is required.")]
        public float OrderItemTotal { get; set; }
        
        public virtual SalesOrder SalesOrder { get; set; }
        public virtual Medicine Product { get; set; }        
    }
}