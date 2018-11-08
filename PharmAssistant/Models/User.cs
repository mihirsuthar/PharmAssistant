using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmAsistant.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(20)]
        [Column(TypeName = "char")]
        [Required]
        public string UserId { get; set; }

        [StringLength(20)]
        [Column(TypeName = "char")]
        [Required]
        public string UserName { get; set; }

        [StringLength(13)]
        [Column(TypeName = "char")]
        [Required]
        public string Role { get; set; }

        [StringLength(50)]
        [Column(TypeName = "char")]
        public string Address { get; set; }

        [Required]
        public long ContactNo { get; set; }

        [StringLength(25)]
        [Column(TypeName = "char")]
        public string EmailId { get; set; }

        [StringLength(10)]
        [Column(TypeName = "char")]
        [Required]
        public string Status { get; set; }

        public virtual ICollection<SalesOrder> SalesOrders { get; set; }
    }
}