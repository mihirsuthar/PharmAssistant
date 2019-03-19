namespace PharmAssistant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DiscountType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MembershipTypes", "PolicyId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MembershipTypes", "PolicyId");
        }
    }
}
