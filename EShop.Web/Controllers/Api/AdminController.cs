using EShop.Domain;
using EShop.Domain.DomainModels;
using EShop.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EShop.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {       
        private readonly IOrderService orderService;

        public AdminController(IOrderService orderService) { 
            this.orderService = orderService;
        }
        [HttpGet("[action]")]
        public List<Order> GetAllActiveOrders()
        {
            return this.orderService.getAllOrders();
        }

        [HttpPost("[action]")]
        public Order GetDetailsForOrder(BaseEntity model)
        {
            return this.orderService.getOrderDetails(model);
        }
    }
}
