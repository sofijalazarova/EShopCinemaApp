﻿using EShop.Domain.Relations;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Domain.DTO
{
    public class ShoppingCartDto
    {
        public List<TicketInShoppingCart> Tickets { get; set; }
        public double TotalPrice { get; set; }
    }
}
