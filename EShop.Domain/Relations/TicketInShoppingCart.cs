using EShop.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Domain.Relations
{
    public class TicketInShoppingCart : BaseEntity
    {
        public Guid MovieId { get; set; }
        public virtual Movie CurrentMovie { get; set; }
        public Guid ShoppingCartId { get; set; }
        public virtual ShoppingCart UserCart { get; set; }
        public int Quantity { get; set; }
    }
}
