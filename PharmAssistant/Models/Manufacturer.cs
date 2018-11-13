using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmAssistant.Models
{
    public class Manufacturer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ManufacturerId { get; set; }

        [StringLength(25)]
        [Column(TypeName = "varchar")]
        [Required]
        public string Name { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar")]
        public string Address { get; set; }

        [Required]
        public long ContactNo { get; set; }

        [StringLength(25)]
        [Column(TypeName = "varchar")]
        public string EmailId { get; set; }

        public virtual ICollection<Medicine> Medicines { get; set; }
    }
}