namespace PharmAssistant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstCommit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        MembershipId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 25, fixedLength: true, unicode: false),
                        Address = c.String(maxLength: 50, fixedLength: true, unicode: false),
                        ContactNumber = c.Long(nullable: false),
                        EmailId = c.String(maxLength: 30, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => t.CustomerId)
                .ForeignKey("dbo.MembershipAccounts", t => t.MembershipId, cascadeDelete: true)
                .Index(t => t.MembershipId);
            
            CreateTable(
                "dbo.MembershipAccounts",
                c => new
                    {
                        MembershipId = c.Int(nullable: false, identity: true),
                        MembershipTypeId = c.Int(nullable: false),
                        JoiningDate = c.DateTime(nullable: false),
                        TotalPurchaseAmount = c.Double(nullable: false),
                        BonusPoints = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MembershipId)
                .ForeignKey("dbo.MembershipTypes", t => t.MembershipTypeId, cascadeDelete: true)
                .Index(t => t.MembershipTypeId);
            
            CreateTable(
                "dbo.MembershipTypes",
                c => new
                    {
                        MembershipTypeId = c.Int(nullable: false, identity: true),
                        MembershipTypeName = c.String(nullable: false, maxLength: 30, fixedLength: true, unicode: false),
                        MembershipTypeDesc = c.String(maxLength: 50, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => t.MembershipTypeId);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        DoctorId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 25, fixedLength: true, unicode: false),
                        Address = c.String(maxLength: 50, fixedLength: true, unicode: false),
                        ContactNo = c.Long(nullable: false),
                        Specilization = c.String(nullable: false, maxLength: 30, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => t.DoctorId);
            
            CreateTable(
                "dbo.SalesOrders",
                c => new
                    {
                        SalesOrderId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Amount = c.Double(nullable: false),
                        Discount = c.Double(nullable: false),
                        Tax = c.Double(nullable: false),
                        GrandTotal = c.Double(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        DoctorId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.SalesOrderId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Doctors", t => t.DoctorId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.DoctorId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.SalesItems",
                c => new
                    {
                        SalesOrderId = c.Int(nullable: false),
                        MedicineId = c.Int(nullable: false),
                        BatchNumber = c.Long(nullable: false),
                        SellingPrice = c.Single(nullable: false),
                        Quantity = c.Int(nullable: false),
                        OrderItemTotal = c.Single(nullable: false),
                    })
                .PrimaryKey(t => new { t.SalesOrderId, t.MedicineId })
                .ForeignKey("dbo.Medicines", t => t.MedicineId, cascadeDelete: true)
                .ForeignKey("dbo.SalesOrders", t => t.SalesOrderId, cascadeDelete: true)
                .Index(t => t.SalesOrderId)
                .Index(t => t.MedicineId);
            
            CreateTable(
                "dbo.Medicines",
                c => new
                    {
                        MedicineId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30, fixedLength: true, unicode: false),
                        ManufacturerId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MedicineId)
                .ForeignKey("dbo.Manufacturers", t => t.ManufacturerId, cascadeDelete: true)
                .ForeignKey("dbo.MedicineCategories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.ManufacturerId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Manufacturers",
                c => new
                    {
                        ManufacturerId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 25, fixedLength: true, unicode: false),
                        Address = c.String(maxLength: 50, fixedLength: true, unicode: false),
                        ContactNo = c.Long(nullable: false),
                        EmailId = c.String(maxLength: 25, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => t.ManufacturerId);
            
            CreateTable(
                "dbo.MedicineCategories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        SupplierId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 25, fixedLength: true, unicode: false),
                        Address = c.String(nullable: false, maxLength: 50, fixedLength: true, unicode: false),
                        ContactNo = c.Long(nullable: false),
                        EmailId = c.String(nullable: false, maxLength: 30, fixedLength: true, unicode: false),
                        Description = c.String(maxLength: 40, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => t.SupplierId);
            
            CreateTable(
                "dbo.PurchaseOrders",
                c => new
                    {
                        PurchaseOrderId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Amount = c.Double(nullable: false),
                        Discount = c.Double(nullable: false),
                        Tax = c.Double(nullable: false),
                        GrandTotal = c.Double(nullable: false),
                        Remarks = c.String(maxLength: 100, fixedLength: true, unicode: false),
                        Payment = c.Boolean(nullable: false),
                        SupplierId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PurchaseOrderId)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId, cascadeDelete: true)
                .Index(t => t.SupplierId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        City = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.PurchaseItems",
                c => new
                    {
                        PurchaseOrderId = c.Int(nullable: false),
                        MedicineId = c.Int(nullable: false),
                        BatchNumber = c.Long(nullable: false),
                        Quantity = c.Int(nullable: false),
                        CostPrice = c.Double(nullable: false),
                        SellingPrice = c.Double(nullable: false),
                        ExpiryDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.PurchaseOrderId, t.MedicineId })
                .ForeignKey("dbo.Medicines", t => t.MedicineId, cascadeDelete: true)
                .ForeignKey("dbo.PurchaseOrders", t => t.PurchaseOrderId, cascadeDelete: true)
                .Index(t => t.PurchaseOrderId)
                .Index(t => t.MedicineId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.StockEntries",
                c => new
                    {
                        StockId = c.Int(nullable: false, identity: true),
                        MedicineId = c.Int(nullable: false),
                        PurchaseOrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StockId)
                .ForeignKey("dbo.Medicines", t => t.MedicineId, cascadeDelete: true)
                .ForeignKey("dbo.PurchaseOrders", t => t.PurchaseOrderId, cascadeDelete: true)
                .Index(t => t.MedicineId)
                .Index(t => t.PurchaseOrderId);
            
            CreateTable(
                "dbo.SupplierMedicines",
                c => new
                    {
                        Supplier_SupplierId = c.Int(nullable: false),
                        Medicine_MedicineId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Supplier_SupplierId, t.Medicine_MedicineId })
                .ForeignKey("dbo.Suppliers", t => t.Supplier_SupplierId, cascadeDelete: true)
                .ForeignKey("dbo.Medicines", t => t.Medicine_MedicineId, cascadeDelete: true)
                .Index(t => t.Supplier_SupplierId)
                .Index(t => t.Medicine_MedicineId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.StockEntries", "PurchaseOrderId", "dbo.PurchaseOrders");
            DropForeignKey("dbo.StockEntries", "MedicineId", "dbo.Medicines");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.PurchaseItems", "PurchaseOrderId", "dbo.PurchaseOrders");
            DropForeignKey("dbo.PurchaseItems", "MedicineId", "dbo.Medicines");
            DropForeignKey("dbo.SalesOrders", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.SalesItems", "SalesOrderId", "dbo.SalesOrders");
            DropForeignKey("dbo.SalesItems", "MedicineId", "dbo.Medicines");
            DropForeignKey("dbo.PurchaseOrders", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.SupplierMedicines", "Medicine_MedicineId", "dbo.Medicines");
            DropForeignKey("dbo.SupplierMedicines", "Supplier_SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.Medicines", "CategoryId", "dbo.MedicineCategories");
            DropForeignKey("dbo.Medicines", "ManufacturerId", "dbo.Manufacturers");
            DropForeignKey("dbo.SalesOrders", "DoctorId", "dbo.Doctors");
            DropForeignKey("dbo.SalesOrders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.MembershipAccounts", "MembershipTypeId", "dbo.MembershipTypes");
            DropForeignKey("dbo.Customers", "MembershipId", "dbo.MembershipAccounts");
            DropIndex("dbo.SupplierMedicines", new[] { "Medicine_MedicineId" });
            DropIndex("dbo.SupplierMedicines", new[] { "Supplier_SupplierId" });
            DropIndex("dbo.StockEntries", new[] { "PurchaseOrderId" });
            DropIndex("dbo.StockEntries", new[] { "MedicineId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.PurchaseItems", new[] { "MedicineId" });
            DropIndex("dbo.PurchaseItems", new[] { "PurchaseOrderId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.PurchaseOrders", new[] { "SupplierId" });
            DropIndex("dbo.Medicines", new[] { "CategoryId" });
            DropIndex("dbo.Medicines", new[] { "ManufacturerId" });
            DropIndex("dbo.SalesItems", new[] { "MedicineId" });
            DropIndex("dbo.SalesItems", new[] { "SalesOrderId" });
            DropIndex("dbo.SalesOrders", new[] { "UserId" });
            DropIndex("dbo.SalesOrders", new[] { "DoctorId" });
            DropIndex("dbo.SalesOrders", new[] { "CustomerId" });
            DropIndex("dbo.MembershipAccounts", new[] { "MembershipTypeId" });
            DropIndex("dbo.Customers", new[] { "MembershipId" });
            DropTable("dbo.SupplierMedicines");
            DropTable("dbo.StockEntries");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.PurchaseItems");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.PurchaseOrders");
            DropTable("dbo.Suppliers");
            DropTable("dbo.MedicineCategories");
            DropTable("dbo.Manufacturers");
            DropTable("dbo.Medicines");
            DropTable("dbo.SalesItems");
            DropTable("dbo.SalesOrders");
            DropTable("dbo.Doctors");
            DropTable("dbo.MembershipTypes");
            DropTable("dbo.MembershipAccounts");
            DropTable("dbo.Customers");
        }
    }
}
