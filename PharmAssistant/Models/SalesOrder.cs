using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmAssistant.Models
{
    public class SalesOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int SalesOrderId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? Date { get; set; }

        [Required(ErrorMessage = "Please enter amount.")]
        public double Amount { get; set; }
        
        public double Discount { get; set; }

        public double Tax { get; set; }

        [Required(ErrorMessage = "Grand total is required.")]
        public double GrandTotal { get; set; }
        
        [Required]
        public int CustomerId { get; set; }

        public int DoctorId { get; set; }

        [StringLength(128)]
        [Column(TypeName = "nvarchar")]
        [Required]
        public string UserId { get; set; }
                
        public virtual Customer Customer { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual AppUser User { get; set; }
        public virtual ICollection<SalesOrderItem> SalesOrderItems { get; set; }
    }
}