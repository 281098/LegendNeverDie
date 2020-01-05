namespace LND.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changestatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderAdmins", "OrderStatus", c => c.String());
            DropColumn("dbo.OrderAdmins", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderAdmins", "Status", c => c.Boolean(nullable: false));
            DropColumn("dbo.OrderAdmins", "OrderStatus");
        }
    }
}
