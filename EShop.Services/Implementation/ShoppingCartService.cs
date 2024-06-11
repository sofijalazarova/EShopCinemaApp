using EShop.Domain.DomainModels;
using EShop.Domain.DTO;
using EShop.Domain.Relations;
using EShop.Repository.Interface;
using EShop.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace EShop.Services.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {

        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<TicketInOrder> _ticketInOrderRepository;
        private readonly IUserRepository _userRepository;

        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository, IUserRepository userRepository, IRepository<Order> orderRepository, IRepository<TicketInOrder> ticketInOrderRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _ticketInOrderRepository = ticketInOrderRepository;
           
        }
        public bool deleteMovieFromSoppingCart(string userId, Guid movieId)
        {
           if (!string.IsNullOrEmpty(userId))
            {
                var loggedInUser = this._userRepository.Get(userId);
                var userCart = loggedInUser.UserCart;
                var itemToDelete = userCart.TicketInShoppingCarts.Where(z => z.MovieId.Equals(movieId)).FirstOrDefault();
                userCart.TicketInShoppingCarts.Remove(itemToDelete);

                this._shoppingCartRepository.Update(userCart);
                return true;        
            }

            return false;
        }

        
        public ShoppingCartDto getShoppingCartInfo(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var loggedInUser = this._userRepository.Get(userId);

                var userCart = loggedInUser.UserCart;

                var allTickets = userCart.TicketInShoppingCarts.ToList();

                var allTicketPrices = allTickets.Select(z => new
                {
                    TicketPrice = z.CurrentMovie.TicketPrice,
                    Quantity = z.Quantity
                }).ToList();

                double totalPrice = 0.0;

                foreach (var item in allTicketPrices)
                {
                    totalPrice += item.Quantity * item.TicketPrice;
                }

                var result = new ShoppingCartDto
                {
                    Tickets = allTickets,
                    TotalPrice = totalPrice
                };

                return result;
            }
            return new ShoppingCartDto();
        }

        public bool order(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var loggedInUser = this._userRepository.Get(userId);
                var userCard = loggedInUser.UserCart;

              
                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    User = loggedInUser,
                    UserId = userId
                };

                this._orderRepository.Insert(order);

                List<TicketInOrder> ticketInOrders = new List<TicketInOrder>();

                var result = userCard.TicketInShoppingCarts.Select(z => new TicketInOrder
                {
                    Id = Guid.NewGuid(),
                    MovieId = z.CurrentMovie.Id,
                    Movie = z.CurrentMovie,
                    OrderId = order.Id,
                    Order = order,
                    Quantity = z.Quantity
                }).ToList();

                StringBuilder sb = new StringBuilder();

                var totalPrice = 0.0;

                sb.AppendLine("Your order is completed. The order conatins: ");

                for (int i = 1; i <= result.Count(); i++)
                {
                    var currentItem = result[i - 1];
                    totalPrice += currentItem.Quantity * currentItem.Movie.TicketPrice;
                    sb.AppendLine(i.ToString() + ". " + currentItem.Movie.MovieTitle + " with quantity of: " + currentItem.Quantity + " and price of: $" + currentItem.Movie.TicketPrice);
                }

                sb.AppendLine("Total price for your order: " + totalPrice.ToString());

              
                ticketInOrders.AddRange(result);

                foreach (var item in ticketInOrders)
                {
                    this._ticketInOrderRepository.Insert(item);
                }

                loggedInUser.UserCart.TicketInShoppingCarts.Clear();

                this._userRepository.Update(loggedInUser);               

                return true;
            }

            return false;
        }
    }
}
