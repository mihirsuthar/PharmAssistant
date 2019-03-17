using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PharmAssistant.Models
{
    public class PharmAssistantContext: IdentityDbContext
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }
        public DbSet<DiscountPolicy> DiscountPolicies { get; set; }
        public DbSet<MembershipAccount> MembershipAccounts { get; set; }
        public DbSet<MembershipDiscount> MembershipDiscounts { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<MedicineCategory> MedicineCategories { get; set; }
        public DbSet<PurchaseOrderItem> PurchaseOrderItems { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<SalesOrderItem> SalesOrderItems { get; set; }
        public DbSet<SalesOrder> SalesOrders { get; set; }
        public DbSet<StockEntry> StockEntries { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Shelf> Shelves { get; set; }

        public System.Data.Entity.DbSet<PharmAssistant.Models.ViewModels.LoginModel> LoginModels { get; set; }

        public System.Data.Entity.DbSet<PharmAssistant.Models.AppUser> IdentityUsers { get; set; }

        public System.Data.Entity.DbSet<PharmAssistant.Models.ViewModels.RoleModificationModel> RoleModificationModels { get; set; }
    }

    public class PharmAssistentDbInit : NullDatabaseInitializer<PharmAssistantContext>
    { }
}