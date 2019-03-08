using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmAssistant.Models.ViewModels
{
    public class InventoryViewModel
    {
        //public Medicine Medicine { get; set; }
        //public StockEntry StockEntry { get; set; }

        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
        public int ReorderLevel { get; set; }
        public int BufferLevel { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string ManufacturerName { get; set; }
        public  string Supplier { get; set; }
        public int StockUnits { get; set; }
        


    }
}