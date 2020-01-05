using LND.Service;
using LND.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LND.Web.Api
{
    [RoutePrefix("api/statistic")]
    [Authorize]
    public class StatisicController : ApiControllerBase
    {
        private IStatisticServie _statisticServie;

        public StatisicController(IErrorService errorService, IStatisticServie statisticServie) : base(errorService)

        {
            this._statisticServie = statisticServie;
        }
        [Route("getrevenue")]
        public HttpResponseMessage GetRevenueStatistic(HttpRequestMessage request,string fromDate,string toDate)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _statisticServie.GetRevenueStatistics(fromDate, toDate);
                HttpResponseMessage respon = request.CreateResponse(HttpStatusCode.OK, model);
                return respon;

            });
        }
    }
}
