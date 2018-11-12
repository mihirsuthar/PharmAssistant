namespace PharmAssistant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_SupplierId_Medicine : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MembershipDiscounts",
                c => new
                    {
                        MembershipId = c.Int(nullable: false, identity: true),
                        DiscountRedeemedDate = c.DateTime(nullable: false),
                        BonusPoints = c.Int(nullable: false),
                        DiscountPercentage = c.Int(nullable: false),
                        DiscountAmount = c.Single(nullable: false),
                        MembershipAccount_MembershipId = c.Int(),
                    })
                .PrimaryKey(t => t.MembershipId)
                .ForeignKey("dbo.MembershipAccounts", t => t.MembershipAccount_MembershipId)
                .Index(t => t.MembershipAccount_MembershipId);
            
            AddColumn("dbo.Medicines", "SupplierId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MembershipDiscounts", "MembershipAccount_MembershipId", "dbo.MembershipAccounts");
            DropIndex("dbo.MembershipDiscounts", new[] { "MembershipAccount_MembershipId" });
            DropColumn("dbo.Medicines", "SupplierId");
            DropTable("dbo.MembershipDiscounts");
        }
    }
}
