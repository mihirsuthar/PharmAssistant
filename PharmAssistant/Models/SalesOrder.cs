using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmAsistant.Models
{
    public class SalesOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int SalesOrderId { get; set; }
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
        [Required]
        public int StockId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public int DoctorId { get; set; }
        [StringLength(20)]
        [Column(TypeName = "char")]
        [Required]
        public string UserId { get; set; }

        public virtual StockEntry StockEntry { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<SalesItem> SalesItems { get; set; }
    }
}