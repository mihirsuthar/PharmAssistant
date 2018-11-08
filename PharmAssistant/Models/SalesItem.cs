using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PharmAsistant.Models
{
    public class SalesItem
    {
        [Required]
        public long BatchNo { get; set; }
        [Required]
        public int Quantity { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Column(Order = 1)]
        public int SalesOrderId { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Column(Order = 2)]
        public int ProductId { get; set; }
        [Required]
        public int StockId { get; set; }

        public virtual SalesOrder SalesOrder { get; set; }
        public virtual Product Product { get; set; }
        public virtual StockEntry StockEntry { get; set; }
    }
}