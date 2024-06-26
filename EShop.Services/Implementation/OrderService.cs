﻿using EShop.Domain;
using EShop.Domain.DomainModels;
using EShop.Repository.Interface;
using EShop.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Services.Implementation
{
    public class OrderService : IOrderService
    {

        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public List<Order> getAllOrders()
        {
            return this._orderRepository.getAllOrders();
        }

        public Order getOrderDetails(BaseEntity model)
        {
            return _orderRepository.getOrderDetails(model);
        }
    }
}
