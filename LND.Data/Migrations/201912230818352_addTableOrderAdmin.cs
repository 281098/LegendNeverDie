namespace LND.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTableOrderAdmin : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderAdmins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(),
                        CustomerAddress = c.String(),
                        CustomerEmail = c.String(),
                        CustomerMobile = c.String(),
                        CustomerMessage = c.String(),
                        ProductName = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantitty = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        PaymentMethod = c.String(),
                        PaymentStatus = c.String(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OrderAdmins");
        }
    }
}
