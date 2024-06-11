using EShop.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Domain.DTO
{
    public class AddToShoppingCartDto
    {
        public Movie SelectedMovie { get; set; }
        public Guid SelectedMovieId { get; set; }
        public int Quantity { get; set; }
    }
}
