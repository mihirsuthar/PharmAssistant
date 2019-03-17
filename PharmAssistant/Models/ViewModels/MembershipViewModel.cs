using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmAssistant.Models.ViewModels
{
    public class MembershipViewModel
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int MembershipId { get; set; }
        public string MembershipType { get; set; }
        public DateTime JoiningDate { get; set; }
        public double TotalPurchase { get; set; }
        public int BonusPoints { get; set; }

    }
}