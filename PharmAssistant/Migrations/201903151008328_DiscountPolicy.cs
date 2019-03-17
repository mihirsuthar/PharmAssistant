namespace PharmAssistant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DiscountPolicy : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DiscountPolicies",
                c => new
                    {
                        PolicyId = c.Int(nullable: false, identity: true),
                        MembershipTypeId = c.Int(nullable: false),
                        UpperBillLimit = c.Double(nullable: false),
                        BonusPoints = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PolicyId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DiscountPolicies");
        }
    }
}
