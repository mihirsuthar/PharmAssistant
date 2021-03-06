﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmAssistant.Models
{
    public class Medicine
    {
        public Medicine()
        {
            this.Suppliers = new HashSet<Supplier>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int MedicineId { get; set; }

        [StringLength(30)]
        [Column(TypeName = "varchar")]
        [Required(ErrorMessage = "Medicine name is required.")]
        public string MedicineName { get; set; }

        //[Required(ErrorMessage = "Please select supplier.")]
        //public int SupplierId { get; set; }

        [Required(ErrorMessage = "Please select manufacturer.")]
        public int ManufacturerId { get; set; }

        [Required(ErrorMessage = "Please select medicine category.")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Shelf Id is required.")]
        public int ShelfId { get; set; }

        [Required(ErrorMessage = "Cost Price is required.")]
        public float CostPrice { get; set; }

        [Required(ErrorMessage = "Selling Price is required.")]
        public float SellingPrice { get; set; }

        [Required(ErrorMessage = "Stock Capacity is required.")]
        public int StockCapacity { get; set; }

        [Required(ErrorMessage = "Reorder Level is required.")]
        public int ReorderLevel { get; set; }

        [Required(ErrorMessage = "Buffer Level is required.")]
        public int BufferLevel { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar")]
        public string Description { get; set; }
        
        public virtual ICollection<Supplier> Suppliers { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        //public virtual Supplier Supplier { get; set; }
        public virtual MedicineCategory MedicineCategory { get; set; }
        public virtual Shelf Shelf { get; set; }
    }
}