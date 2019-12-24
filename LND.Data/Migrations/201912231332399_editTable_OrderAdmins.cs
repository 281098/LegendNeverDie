namespace LND.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editTable_OrderAdmins : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderAdmins", "TotalPrice", c => c.String());
            AlterColumn("dbo.OrderAdmins", "Price", c => c.String());
            AlterColumn("dbo.OrderAdmins", "Quantitty", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OrderAdmins", "Quantitty", c => c.Int(nullable: false));
            AlterColumn("dbo.OrderAdmins", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.OrderAdmins", "TotalPrice");
        }
    }
}
