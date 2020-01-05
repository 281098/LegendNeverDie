namespace LND.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmorepricein : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderAdmins", "TotalPriceIn", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderAdmins", "TotalPriceIn");
        }
    }
}
