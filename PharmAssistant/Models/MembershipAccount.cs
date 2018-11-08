using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmAsistant.Models
{
    public class MembershipAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int MembershipId { get; set; }
        [Required]
        public DateTime JoiningDate { get; set; }
        [StringLength(20)]
        [Column(TypeName = "char")]
        [Required]
        public string Type { get; set; }
        public double TotalPurchaseAmount { get; set; }
        public int BonusPoints { get; set; }
        public DateTime LastDiscountRedeemedDate { get; set; }
        public float LastDiscountRedeemed { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
    }
}