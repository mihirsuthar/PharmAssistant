using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PharmAssistant.Models
{
    public class MembershipDiscount
    {
        [Required(ErrorMessage = "Please select Membership Id.")]
        public int MembershipId { get; set; }

        [Required(ErrorMessage = "Please select Date.")]
        public DateTime DiscountRedeemedDate { get; set; }

        [Required(ErrorMessage = "Please enter bonus points.")]
        public int BonusPoints { get; set; }

        [Required(ErrorMessage = "Please enter discount percentages.")]
        public int DiscountPercentage { get; set; }

        [Required(ErrorMessage = "Please enter discount amount.")]
        public float DiscountAmount { get; set; }

        public virtual MembershipAccount MembershipAccount { get; set; }
        
    }
}