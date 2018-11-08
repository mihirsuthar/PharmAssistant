using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmAsistant.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ProductId { get; set; }
        [StringLength(30)]
        [Column(TypeName = "char")]
        [Required]
        public string Name { get; set; }
        [Required]
        public int ManufacturerId { get; set; }
        [Required]
        public int CategoryId { get; set; }

        public virtual ICollection<Supplier> Suppliers { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
    }
}