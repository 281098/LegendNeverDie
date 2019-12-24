using LND.Data.Infrastructure;
using LND.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LND.Data.Repositories
{
    public interface IOrderAdminRepository : IRepository<OrderAdmin>
    {
    }

    public class OrderAdminRepository : RepositoryBase<OrderAdmin>, IOrderAdminRepository
    {
        public OrderAdminRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
