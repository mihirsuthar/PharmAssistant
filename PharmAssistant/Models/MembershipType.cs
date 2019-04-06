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
        [Column(TypeName = "varchar")]
        [Required(ErrorMessage = "Membership type name id is required.")]
        public string MembershipTypeName { get; set; }
        
        [StringLength(50)]
        [Column(TypeName = "varchar")]
        public string MembershipTypeDesc { get; set; }

        [Required(ErrorMessage = "Upper Bill limit is required.")]
        public double UpperBillLimit { get; set; }

        [Required(ErrorMessage = "Bonus points are required.")]
        public int BonusPoints { get; set; }

    }
}