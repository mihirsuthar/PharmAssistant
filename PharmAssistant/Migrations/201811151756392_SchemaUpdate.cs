namespace PharmAssistant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SchemaUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PurchaseItems", "PurchaseOrderId", "dbo.PurchaseOrders");
            DropForeignKey("dbo.StockEntries", "PurchaseOrderId", "dbo.PurchaseOrders");
            DropIndex("dbo.PurchaseItems", new[] { "PurchaseOrderId" });
            DropIndex("dbo.StockEntries", new[] { "PurchaseOrderId" });
            DropPrimaryKey("dbo.PurchaseOrders");
            DropPrimaryKey("dbo.PurchaseItems");
            AddColumn("dbo.PurchaseItems", "MedicineName", c => c.String(maxLength: 30, unicode: false));
            AddColumn("dbo.PurchaseItems", "PurchaseOrder_PurchaseOrderId", c => c.Long());
            AddColumn("dbo.StockEntries", "PurchaseOrder_PurchaseOrderId", c => c.Long());
            AlterColumn("dbo.PurchaseOrders", "PurchaseOrderId", c => c.Long(nullable: false));
            AlterColumn("dbo.PurchaseItems", "PurchaseOrderId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.PurchaseOrders", "PurchaseOrderId");
            AddPrimaryKey("dbo.PurchaseItems", new[] { "PurchaseOrderId", "MedicineId" });
            CreateIndex("dbo.PurchaseItems", "PurchaseOrder_PurchaseOrderId");
            CreateIndex("dbo.StockEntries", "PurchaseOrder_PurchaseOrderId");
            AddForeignKey("dbo.PurchaseItems", "PurchaseOrder_PurchaseOrderId", "dbo.PurchaseOrders", "PurchaseOrderId");
            AddForeignKey("dbo.StockEntries", "PurchaseOrder_PurchaseOrderId", "dbo.PurchaseOrders", "PurchaseOrderId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StockEntries", "PurchaseOrder_PurchaseOrderId", "dbo.PurchaseOrders");
            DropForeignKey("dbo.PurchaseItems", "PurchaseOrder_PurchaseOrderId", "dbo.PurchaseOrders");
            DropIndex("dbo.StockEntries", new[] { "PurchaseOrder_PurchaseOrderId" });
            DropIndex("dbo.PurchaseItems", new[] { "PurchaseOrder_PurchaseOrderId" });
            DropPrimaryKey("dbo.PurchaseItems");
            DropPrimaryKey("dbo.PurchaseOrders");
            AlterColumn("dbo.PurchaseItems", "PurchaseOrderId", c => c.Int(nullable: false));
            AlterColumn("dbo.PurchaseOrders", "PurchaseOrderId", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.StockEntries", "PurchaseOrder_PurchaseOrderId");
            DropColumn("dbo.PurchaseItems", "PurchaseOrder_PurchaseOrderId");
            DropColumn("dbo.PurchaseItems", "MedicineName");
            AddPrimaryKey("dbo.PurchaseItems", new[] { "PurchaseOrderId", "MedicineId" });
            AddPrimaryKey("dbo.PurchaseOrders", "PurchaseOrderId");
            CreateIndex("dbo.StockEntries", "PurchaseOrderId");
            CreateIndex("dbo.PurchaseItems", "PurchaseOrderId");
            AddForeignKey("dbo.StockEntries", "PurchaseOrderId", "dbo.PurchaseOrders", "PurchaseOrderId", cascadeDelete: true);
            AddForeignKey("dbo.PurchaseItems", "PurchaseOrderId", "dbo.PurchaseOrders", "PurchaseOrderId", cascadeDelete: true);
        }
    }
}
