namespace PharmAssistant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class databaseSchemaUpdated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Shelves",
                c => new
                    {
                        ShelfId = c.Int(nullable: false, identity: true),
                        ShelfName = c.String(nullable: false, maxLength: 30, fixedLength: true, unicode: false),
                        ShelfNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ShelfId);
            
            CreateTable(
                "dbo.LoginModels",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Name);
            
            CreateTable(
                "dbo.RoleModificationModels",
                c => new
                    {
                        RoleName = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.RoleName);
            
            AddColumn("dbo.Medicines", "ShelfId", c => c.Int(nullable: false));
            AddColumn("dbo.Medicines", "CostPrice", c => c.Single(nullable: false));
            AddColumn("dbo.Medicines", "SellingPrice", c => c.Single(nullable: false));
            AddColumn("dbo.Medicines", "Description", c => c.String(maxLength: 50, fixedLength: true, unicode: false));
            AddColumn("dbo.PurchaseOrders", "PurchaseOrderCode", c => c.String(maxLength: 32, fixedLength: true, unicode: false));
            AddColumn("dbo.PurchaseOrders", "PurchaseDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.PurchaseOrders", "OrderCost", c => c.Double(nullable: false));
            AddColumn("dbo.PurchaseOrders", "AmountPaid", c => c.Double(nullable: false));
            AddColumn("dbo.PurchaseOrders", "PaymentMode", c => c.String(maxLength: 25, fixedLength: true, unicode: false));
            AddColumn("dbo.PurchaseOrders", "PaymentStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.StockEntries", "BatchNumber", c => c.String());
            AddColumn("dbo.StockEntries", "Quantity", c => c.Int(nullable: false));
            AddColumn("dbo.StockEntries", "CostPrice", c => c.Single(nullable: false));
            AddColumn("dbo.StockEntries", "SellingPrice", c => c.Single(nullable: false));
            AddColumn("dbo.StockEntries", "ExpiryDate", c => c.DateTime(nullable: false));
            CreateIndex("dbo.Medicines", "ShelfId");
            AddForeignKey("dbo.Medicines", "ShelfId", "dbo.Shelves", "ShelfId", cascadeDelete: true);
            DropColumn("dbo.PurchaseOrders", "Date");
            DropColumn("dbo.PurchaseOrders", "Amount");
            DropColumn("dbo.PurchaseOrders", "GrandTotal");
            DropColumn("dbo.PurchaseOrders", "Payment");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PurchaseOrders", "Payment", c => c.Boolean(nullable: false));
            AddColumn("dbo.PurchaseOrders", "GrandTotal", c => c.Double(nullable: false));
            AddColumn("dbo.PurchaseOrders", "Amount", c => c.Double(nullable: false));
            AddColumn("dbo.PurchaseOrders", "Date", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.Medicines", "ShelfId", "dbo.Shelves");
            DropIndex("dbo.Medicines", new[] { "ShelfId" });
            DropColumn("dbo.StockEntries", "ExpiryDate");
            DropColumn("dbo.StockEntries", "SellingPrice");
            DropColumn("dbo.StockEntries", "CostPrice");
            DropColumn("dbo.StockEntries", "Quantity");
            DropColumn("dbo.StockEntries", "BatchNumber");
            DropColumn("dbo.PurchaseOrders", "PaymentStatus");
            DropColumn("dbo.PurchaseOrders", "PaymentMode");
            DropColumn("dbo.PurchaseOrders", "AmountPaid");
            DropColumn("dbo.PurchaseOrders", "OrderCost");
            DropColumn("dbo.PurchaseOrders", "PurchaseDate");
            DropColumn("dbo.PurchaseOrders", "PurchaseOrderCode");
            DropColumn("dbo.Medicines", "Description");
            DropColumn("dbo.Medicines", "SellingPrice");
            DropColumn("dbo.Medicines", "CostPrice");
            DropColumn("dbo.Medicines", "ShelfId");
            DropTable("dbo.RoleModificationModels");
            DropTable("dbo.LoginModels");
            DropTable("dbo.Shelves");
        }
    }
}
