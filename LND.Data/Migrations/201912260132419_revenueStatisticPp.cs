namespace LND.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class revenueStatisticPp : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure("GetRevenueStatistic",
                p => new
                {
                    fromDate = p.String(),
                    toDate = p.String()
                },
                @"
                    SELECT o.CreatedDate,
                    SUM(od.Quantitty*od.Price) AS Revenues,
                    SUM((od.Quantitty*od.Price)-(od.Quantitty*p.PriceIn)) AS Benefit 
                    FROM dbo.Orders o
                    INNER JOIN dbo.OrderDetails od
                    ON	od.OrderID = o.ID
                    INNER JOIN dbo.Products p
                    ON	p.ID = od.ProductID
                    WHERE o.CreatedDate<=CAST(@toDate AS DATE) AND o.CreatedDate >=CAST(@fromDate AS date)
                    GROUP BY o.CreatedDate"
                ); 
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.GetRevenueStatistic");
        }
    }
}
