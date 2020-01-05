using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LND.Common.ViewModels
{
   public class RevenueStatistic
    {
        public DateTime Date { get; set; }
        public decimal Revenues { get; set; }
        public decimal Benefit { get; set; }
    }
}
