namespace PharmAssistant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MedicineModelUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Medicines", "StockCapacity", c => c.Int(nullable: false));
            AddColumn("dbo.Medicines", "ReorderLevel", c => c.Int(nullable: false));
            AddColumn("dbo.Medicines", "BufferLevel", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Medicines", "BufferLevel");
            DropColumn("dbo.Medicines", "ReorderLevel");
            DropColumn("dbo.Medicines", "StockCapacity");
        }
    }
}
