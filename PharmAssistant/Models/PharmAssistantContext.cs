using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PharmAsistant.Models
{
    public class PharmAssistantContext: DbContext
    {
        public PharmAssistantContext():base("PharmAssistant")
        {}

        public static PharmAssistantContext Create()
        {
            return new PharmAssistantContext();
        }

        static PharmAssistantContext()
        {
            Database.SetInitializer<PharmAssistantContext>(new PharmAssistentDbInit());
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<MembershipAccount> MembershipAccounts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<PurchaseItem> PurchaseItems { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<SalesItem> SalesItems { get; set; }
        public DbSet<SalesOrder> SalesOrders { get; set; }
        public DbSet<StockEntry> StockEntries { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<User> Users { get; set; }
    }

    public class PharmAssistentDbInit : NullDatabaseInitializer<PharmAssistantContext>
    { }
}