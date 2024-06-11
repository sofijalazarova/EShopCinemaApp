using EShop.Domain.DomainModels;
using EShop.Domain.DTO;
using EShop.Domain.Relations;
using EShop.Repository.Interface;
using EShop.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EShop.Services.Implementation
{
    public class MovieService : IMovieService
    {
        private readonly IRepository<Movie> movieRepository;
        private readonly IRepository<TicketInShoppingCart> _ticketInShoppingCartRepository;
        private readonly IUserRepository _userRepository;

        public MovieService(IRepository<Movie> movieRepository , IUserRepository userRepository, IRepository<TicketInShoppingCart> ticketInShoppingCartRepository)
        {
            this.movieRepository = movieRepository;
            _userRepository = userRepository;
            this._ticketInShoppingCartRepository = ticketInShoppingCartRepository;
        }

        // TO DO:
        public bool AddToShoppingCart(AddToShoppingCartDto item, string userID)
        {
            var user = this._userRepository.Get(userID);

            var userShoppingCard = user.UserCart;

            if (item.SelectedMovieId != null && userShoppingCard != null)
            {
                var product = this.GetDetailsForMovie(item.SelectedMovieId);
                //{896c1325-a1bb-4595-92d8-08da077402fc}

                if (product != null)
                {
                    TicketInShoppingCart itemToAdd = new TicketInShoppingCart
                    {
                        Id = Guid.NewGuid(),
                        CurrentMovie = product,
                        MovieId = product.Id,
                        UserCart = userShoppingCard,
                        ShoppingCartId = userShoppingCard.Id,
                        Quantity = item.Quantity
                    };

                    var existing = userShoppingCard.TicketInShoppingCarts.Where(z => z.ShoppingCartId == userShoppingCard.Id && z.MovieId == itemToAdd.MovieId).FirstOrDefault();

                    if (existing != null)
                    {
                        existing.Quantity += itemToAdd.Quantity;
                        this._ticketInShoppingCartRepository.Update(existing);

                    }
                    else
                    {
                        this._ticketInShoppingCartRepository.Insert(itemToAdd);
                    }
                    return true;
                }
                return false;
            }
            return false;
        }

        public void CreateNewMovie(Movie m)
        {
            this.movieRepository.Insert(m);
        }

        public void DeleteMovie(Guid id)
        {
            var movie = this.GetDetailsForMovie(id);
            this.movieRepository.Delete(movie);
        }

        public List<Movie> GetAllMovies()
        {
            return this.movieRepository.GetAll().ToList();
        }

        public Movie GetDetailsForMovie(Guid? id)
        {
            return this.movieRepository.Get(id);
        }

        
        public AddToShoppingCartDto GetShoppingCartInfo(Guid? id)
        {
            var movie = this.GetDetailsForMovie(id);
            AddToShoppingCartDto model = new AddToShoppingCartDto
            {
                SelectedMovie = movie,
                SelectedMovieId = movie.Id,
                Quantity = 1
            };

            return model;
        }

        public void UpdeteExistingProduct(Movie m)
        {
            this.movieRepository.Update(m);
        }
    }
}
