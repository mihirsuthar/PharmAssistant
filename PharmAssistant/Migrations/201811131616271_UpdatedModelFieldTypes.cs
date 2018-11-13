namespace PharmAssistant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedModelFieldTypes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "Name", c => c.String(nullable: false, maxLength: 25, unicode: false));
            AlterColumn("dbo.Customers", "Address", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.Customers", "EmailId", c => c.String(maxLength: 30, unicode: false));
            AlterColumn("dbo.MembershipTypes", "MembershipTypeName", c => c.String(nullable: false, maxLength: 30, unicode: false));
            AlterColumn("dbo.MembershipTypes", "MembershipTypeDesc", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.Doctors", "Name", c => c.String(nullable: false, maxLength: 25, unicode: false));
            AlterColumn("dbo.Doctors", "Address", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.Doctors", "Specilization", c => c.String(nullable: false, maxLength: 30, unicode: false));
            AlterColumn("dbo.Medicines", "Name", c => c.String(nullable: false, maxLength: 30, unicode: false));
            AlterColumn("dbo.Medicines", "Description", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.Manufacturers", "Name", c => c.String(nullable: false, maxLength: 25, unicode: false));
            AlterColumn("dbo.Manufacturers", "Address", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.Manufacturers", "EmailId", c => c.String(maxLength: 25, unicode: false));
            AlterColumn("dbo.MedicineCategories", "Name", c => c.String(nullable: false, maxLength: 30, unicode: false));
            AlterColumn("dbo.Shelves", "ShelfName", c => c.String(nullable: false, maxLength: 30, unicode: false));
            AlterColumn("dbo.Suppliers", "Name", c => c.String(nullable: false, maxLength: 25, unicode: false));
            AlterColumn("dbo.Suppliers", "Address", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.Suppliers", "EmailId", c => c.String(nullable: false, maxLength: 30, unicode: false));
            AlterColumn("dbo.Suppliers", "Description", c => c.String(maxLength: 40, unicode: false));
            AlterColumn("dbo.PurchaseOrders", "PurchaseOrderCode", c => c.String(maxLength: 32, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PurchaseOrders", "PurchaseOrderCode", c => c.String(maxLength: 32, fixedLength: true, unicode: false));
            AlterColumn("dbo.Suppliers", "Description", c => c.String(maxLength: 40, fixedLength: true, unicode: false));
            AlterColumn("dbo.Suppliers", "EmailId", c => c.String(nullable: false, maxLength: 30, fixedLength: true, unicode: false));
            AlterColumn("dbo.Suppliers", "Address", c => c.String(nullable: false, maxLength: 50, fixedLength: true, unicode: false));
            AlterColumn("dbo.Suppliers", "Name", c => c.String(nullable: false, maxLength: 25, fixedLength: true, unicode: false));
            AlterColumn("dbo.Shelves", "ShelfName", c => c.String(nullable: false, maxLength: 30, fixedLength: true, unicode: false));
            AlterColumn("dbo.MedicineCategories", "Name", c => c.String(nullable: false, maxLength: 30, fixedLength: true, unicode: false));
            AlterColumn("dbo.Manufacturers", "EmailId", c => c.String(maxLength: 25, fixedLength: true, unicode: false));
            AlterColumn("dbo.Manufacturers", "Address", c => c.String(maxLength: 50, fixedLength: true, unicode: false));
            AlterColumn("dbo.Manufacturers", "Name", c => c.String(nullable: false, maxLength: 25, fixedLength: true, unicode: false));
            AlterColumn("dbo.Medicines", "Description", c => c.String(maxLength: 50, fixedLength: true, unicode: false));
            AlterColumn("dbo.Medicines", "Name", c => c.String(nullable: false, maxLength: 30, fixedLength: true, unicode: false));
            AlterColumn("dbo.Doctors", "Specilization", c => c.String(nullable: false, maxLength: 30, fixedLength: true, unicode: false));
            AlterColumn("dbo.Doctors", "Address", c => c.String(maxLength: 50, fixedLength: true, unicode: false));
            AlterColumn("dbo.Doctors", "Name", c => c.String(nullable: false, maxLength: 25, fixedLength: true, unicode: false));
            AlterColumn("dbo.MembershipTypes", "MembershipTypeDesc", c => c.String(maxLength: 50, fixedLength: true, unicode: false));
            AlterColumn("dbo.MembershipTypes", "MembershipTypeName", c => c.String(nullable: false, maxLength: 30, fixedLength: true, unicode: false));
            AlterColumn("dbo.Customers", "EmailId", c => c.String(maxLength: 30, fixedLength: true, unicode: false));
            AlterColumn("dbo.Customers", "Address", c => c.String(maxLength: 50, fixedLength: true, unicode: false));
            AlterColumn("dbo.Customers", "Name", c => c.String(nullable: false, maxLength: 25, fixedLength: true, unicode: false));
        }
    }
}
