using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmAssistant.Models
{
    public class MembershipAccount
    {        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MembershipId { get; set; }

        public int CustomerId { get; set; }        

        [Required(ErrorMessage = "Please select membership type.")]
        public int MembershipTypeId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? JoiningDate { get; set; }        
        
        public double TotalPurchaseAmount { get; set; }
        public int BonusPoints { get; set; }

        //public DateTime LastDiscountRedeemedDate { get; set; }
        //public float LastDiscountRedeemed { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        [ForeignKey("MembershipTypeId")]
        public virtual MembershipType MembershipType { get; set; }
    }
}