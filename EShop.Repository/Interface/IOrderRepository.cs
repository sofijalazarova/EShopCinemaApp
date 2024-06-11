using EShop.Domain.DomainModels;
using EShop.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Repository.Interface
{
    public interface IOrderRepository
    {
        public List<Order> getAllOrders();
        public Order getOrderDetails(BaseEntity model);
    }
}
