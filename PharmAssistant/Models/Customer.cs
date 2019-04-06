using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PharmAssistant.Validators;

namespace PharmAssistant.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }

        public int MembershipId { get; set; }

        [StringLength(25, ErrorMessage = "Name can have maximum 25 characters.")]
        [Column(TypeName = "varchar")]
        [Required(ErrorMessage = "Customer name is required.")]
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [StringLength(50, ErrorMessage = "Address can have maximim 50 characters.")]
        [Column(TypeName = "varchar")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Contact number id is required.")]
        [Display(Name = "Contact Number")]
        public long ContactNumber { get; set; }

        [StringLength(30, ErrorMessage = "Email Id can have maximum 30 characters.")]
        [Column(TypeName = "varchar")]
        [Display(Name = "Email Id")]
        [ValidateEmail(ErrorMessage = "Please enter valid Email Id.")]
        public string EmailId { get; set; }
                
        //[Display(Name = "Status")]
        //public bool Status { get; set; }

        //public virtual MembershipAccount MembershipAccount { get; set; }
    }
}