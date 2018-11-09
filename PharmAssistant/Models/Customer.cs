using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmAssistant.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int CustomerId { get; set; }

        public int MembershipId { get; set; }

        [StringLength(25)]
        [Column(TypeName = "char")]
        [Required(ErrorMessage = "Customer name is required.")]
        public string Name { get; set; }

        [StringLength(50)]
        [Column(TypeName = "char")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Contact number id is required.")]
        public long ContactNumber { get; set; }

        [StringLength(30)]
        [Column(TypeName = "char")]
        public string EmailId { get; set; }

        public virtual MembershipAccount MembershipAccount { get; set; }
    }
}