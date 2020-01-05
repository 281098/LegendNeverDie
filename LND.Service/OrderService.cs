using LND.Data.Infrastructure;
using LND.Data.Repositories;
using LND.Model.Models;
using System;
using System.Collections.Generic;

namespace LND.Service
{
    public interface IOrderService
    {
        bool Create(Order order, List<OrderDetail> orderDetails);
        bool CreateOrderAdmin(OrderAdmin orderAdmin);
        IEnumerable<OrderAdmin> GetAll(string keyword);
        OrderAdmin Delete(int id);
        void Transfer(int id);
        void Complete(int id);
        void Save();
    }

    public class OrderService : IOrderService
    {
        private IOrderRepository _orderRepository;
        private IOrderAdminRepository _orderAdminRepository;
        private IOrderDetailRepository _orderDetailRepository;
        private IUnitOfWork _unitOfWork;

        public OrderService(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository, IOrderAdminRepository orderAdminRepository, IUnitOfWork unitOfWork)
        {
            this._orderRepository = orderRepository;
            this._orderDetailRepository = orderDetailRepository;
            this._orderAdminRepository = orderAdminRepository;
            this._unitOfWork = unitOfWork;
        }

        public void Complete(int id)
        {
            var order = _orderAdminRepository.GetSingleById(id);
            order.PaymentStatus = "Đã thanh toán";
            order.OrderStatus = "Đã nhận hàng";

            _orderAdminRepository.Update(order);
        }

        public bool Create(Order order, List<OrderDetail> orderDetails)
        {
            try
            {
                _orderRepository.Add(order);
                _unitOfWork.Commit();

                foreach (var orderDetail in orderDetails)
                {
                    orderDetail.OrderID = order.ID;
                    _orderDetailRepository.Add(orderDetail);
                }
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool CreateOrderAdmin(OrderAdmin orderAdmin)
        {
            try
            {                            
                _orderAdminRepository.Add(orderAdmin);           
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public OrderAdmin Delete(int id)
        {
            return _orderAdminRepository.Delete(id);
        }

        public IEnumerable<OrderAdmin> GetAll()
        {
            return _orderAdminRepository.GetAll();
        }

        public IEnumerable<OrderAdmin> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                
                return _orderAdminRepository.GetMulti(x => x.CustomerName.Contains(keyword) || x.CustomerMobile.Contains(keyword)
                                                       || x.CustomerAddress.Contains(keyword)|| x.PaymentMethod.Contains(keyword)
                                                       ||x.OrderStatus.Contains(keyword));
            }
              
            else
                return _orderAdminRepository.GetAll();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Transfer(int id)
        {
            var order = _orderAdminRepository.GetSingleById(id);
            order.OrderStatus = "Đơn hàng đã được duyệt, đang trong quá trình vận chuyển.";

          _orderAdminRepository.Update(order);
        }
    }
}