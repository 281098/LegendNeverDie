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
        bool CreateOrderAdmin(List<OrderAdmin> orderAdmin);
        IEnumerable<OrderAdmin> GetAll(string keyword);
        OrderAdmin Delete(int id);
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

        public bool CreateOrderAdmin(List<OrderAdmin> orderAdmin)
        {
            try
            {
                foreach(var item in orderAdmin)
                {
                    _orderAdminRepository.Add(item);
                }                
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
                return _orderAdminRepository.GetMulti(x => x.CustomerName.Contains(keyword) || x.CustomerMobile.Contains(keyword));
            }
              
            else
                return _orderAdminRepository.GetAll();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}