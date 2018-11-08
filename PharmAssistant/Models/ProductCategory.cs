using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmAsistant.Models
{
    public class ProductCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int CategoryId { get; set; }
        [StringLength(30)]
        [Column(TypeName = "char")]
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}