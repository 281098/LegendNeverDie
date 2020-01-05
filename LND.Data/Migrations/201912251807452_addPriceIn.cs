namespace LND.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPriceIn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "PriceIn", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "PriceIn");
        }
    }
}
