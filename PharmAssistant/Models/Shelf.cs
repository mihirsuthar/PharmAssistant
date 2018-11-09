using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PharmAssistant.Models
{
    public class Shelf
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ShelfId { get; set; }

        [Required(ErrorMessage = "Shelf name is required.") ]
        [Column(TypeName = "char")]
        [StringLength(30)]        
        public string ShelfName { get; set; }

        [Required(ErrorMessage = "Shelf number is required.")]
        public int ShelfNumber { get; set; }
    }
}