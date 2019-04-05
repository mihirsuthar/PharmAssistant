namespace PharmAssistant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MembershipAccount : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "MembershipId", "dbo.MembershipAccounts");
            DropIndex("dbo.Customers", new[] { "MembershipId" });
            AddColumn("dbo.MembershipAccounts", "CustomerId", c => c.Int(nullable: false));
            CreateIndex("dbo.MembershipAccounts", "CustomerId");
            AddForeignKey("dbo.MembershipAccounts", "CustomerId", "dbo.Customers", "CustomerId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MembershipAccounts", "CustomerId", "dbo.Customers");
            DropIndex("dbo.MembershipAccounts", new[] { "CustomerId" });
            DropColumn("dbo.MembershipAccounts", "CustomerId");
            CreateIndex("dbo.Customers", "MembershipId");
            AddForeignKey("dbo.Customers", "MembershipId", "dbo.MembershipAccounts", "MembershipId", cascadeDelete: true);
        }
    }
}
