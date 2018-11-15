using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PharmAssistant.Models.ViewModels
{
    public class PurchaseOrderViewModel
    {
        [Key]
        public PurchaseOrder PurchaseOrder { get; set; }
        public PurchaseOrderItem PurchaseOrderItem { get; set; }
        public ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; }
    }
}