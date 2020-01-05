using LND.Common.ViewModels;
using LND.Data.Infrastructure;
using LND.Model.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace LND.Data.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        IEnumerable<RevenueStatistic> GetRevenuStatistic(string fromDate, string toDate);
    }

    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<RevenueStatistic> GetRevenuStatistic(string fromDate, string toDate)
        {
            var para = new SqlParameter[]
            {
                new SqlParameter("@fromDate",fromDate),
                new SqlParameter("@toDate",toDate),
            };
            return DbContext.Database.SqlQuery<RevenueStatistic>("GetRevenueStatistic @fromDate,@toDate", para);
        }
    }
}