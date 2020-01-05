using LND.Common.ViewModels;
using LND.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LND.Service
{
    public interface IStatisticServie
    {
        IEnumerable<RevenueStatistic> GetRevenueStatistics(string fromDate, string toDate);
    }
    public class StatisicService : IStatisticServie
    {
        IOrderRepository _orderRepository;
        public StatisicService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;

        }
        public IEnumerable<RevenueStatistic> GetRevenueStatistics(string fromDate, string toDate)
        {
            return _orderRepository.GetRevenuStatistic(fromDate, toDate);
        }
    }
}
