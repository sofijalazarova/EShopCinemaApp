using EShop.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Domain.Relations
{
    public class TicketInOrder : BaseEntity
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Guid MovieId { get; set; }
        public Movie Movie { get; set; }
        public int Quantity { get; set; }
    }
}
