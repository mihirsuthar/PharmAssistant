using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmAsistant.Models
{
    public class Doctor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int DoctorId { get; set; }
        [StringLength(25)]
        [Column(TypeName = "char")]
        [Required]
        public string Name { get; set; }
        [StringLength(50)]
        [Column(TypeName = "char")]
        public string Address { get; set; }
        public long ContactNo { get; set; }
        [StringLength(30)]
        [Column(TypeName = "char")]
        [Required]
        public string Specilization { get; set; }

        public virtual ICollection<SalesOrder> SalesOrders { get; set; }
    }
}