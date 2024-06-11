using EShop.Domain.Relations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EShop.Domain.DomainModels
{
    public class Movie : BaseEntity
    {
        [Required]
        public string MovieTitle { get; set; }
        [Required]
        public string MovieImage { get; set; }
        [Required]
        public string MovieCoverImage { get; set; }
        [Required]
        public string MovieDescription { get; set; }
        [Required]
        public string MovieDirector { get; set; }
        public string MovieGenre { get; set; }
        [Required]
        public double TicketPrice { get; set; }
        [Required]
        public double MovieRating { get; set; }
        [Required]
        public double MovieYear { get; set; }
        [Required]
        public double MovieDuration { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }


        public virtual ICollection<TicketInShoppingCart> TicketInShoppingCarts { get; set; }
        public virtual ICollection<TicketInOrder> TicketInOrders { get; set; }
        
    }
}
