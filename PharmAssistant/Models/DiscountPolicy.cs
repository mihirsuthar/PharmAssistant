using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PharmAssistant.Models
{
    public class DiscountPolicy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int PolicyId { get; set; }
        [Required(ErrorMessage = "Membership Type is required.")]
        public int MembershipTypeId{ get; set; }
        [Required(ErrorMessage = "Upper Bill limit is required.")]
        public double UpperBillLimit { get; set; }
        [Required(ErrorMessage = "Bonus points are required.")]
        public int BonusPoints { get; set; }

        public MembershipType membershipType { get; set; }

    }
}