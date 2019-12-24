using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using LND.Model.Models;
using LND.Service;
using LND.Web.Infrastructure.Core;
using LND.Web.Infrastructure.Extensions;
using LND.Web.Models;

namespace LND.Web.Api
{
    [RoutePrefix("api/order")]
    [Authorize]
    public class OrderController : ApiControllerBase
    {
        #region Initialize
        private IOrderService _orderService;

        public OrderController(IErrorService errorService, IOrderService orderService)
            : base(errorService)
        {
            this._orderService = orderService;
        }

        #endregion
       
        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _orderService.GetAll(keyword);

                totalRow = model.Count();
                var responseData = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);

                var paginationSet = new PaginationSet<OrderAdmin>()
                {
                    Items = responseData,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };
                var response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }
        [Route("delete")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var oldProductCategory = _orderService.Delete(id);
                    _orderService.Save();

                    var responseData = oldProductCategory;
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }
    }
}
