using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PharmAssistant.Models
{
    public class MembershipType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int MembershipTypeId { get; set; }

        [StringLength(30)]
        [Column(TypeName = "char")]
        [Required(ErrorMessage = "Membership type name id is required.")]
        public string MembershipTypeName { get; set; }

        [StringLength(50)]
        [Column(TypeName = "char")]
        public string MembershipTypeDesc { get; set; }
    }
}