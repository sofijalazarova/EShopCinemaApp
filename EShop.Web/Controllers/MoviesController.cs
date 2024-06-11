using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EShop.Domain.DomainModels;
using EShop.Repository;
using EShop.Services.Interface;
using EShop.Domain.DTO;
using System.Security.Claims;
using EShop.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace EShop.Web.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<EShopApplicationUser> signInManager;

        public MoviesController(IMovieService movieService, SignInManager<EShopApplicationUser> signInManager)
        {
            this._movieService = movieService;
            this.signInManager = signInManager;
        }

        // GET: Movies
        public IActionResult Index()
        {
            return View(this._movieService.GetAllMovies());
        }

        // GET: Movies/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = this._movieService.GetDetailsForMovie(id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("MovieTitle,MovieImage,MovieCoverImage,MovieDescription,MovieDirector,MovieGenre,TicketPrice,MovieRating,MovieYear,MovieDuration,ReleaseDate,Id")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                movie.Id = Guid.NewGuid();
                this._movieService.CreateNewMovie(movie);
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = this._movieService.GetDetailsForMovie(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("MovieTitle,MovieImage,MovieCoverImage,MovieDescription,MovieDirector,MovieGenre,TicketPrice,MovieRating,MovieYear,MovieDuration,ReleaseDate,Id")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this._movieService.UpdeteExistingProduct(movie);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = this._movieService.GetDetailsForMovie(id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            this._movieService.DeleteMovie(id);
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(Guid id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }

        public IActionResult AddTicketToCart(Guid id)
        {

            if (signInManager.IsSignedIn(User))
            {
                var result = this._movieService.GetShoppingCartInfo(id);
                return View(result);
            }

            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddTicketToCart(AddToShoppingCartDto model)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = this._movieService.AddToShoppingCart(model, userId);

            if (result)
            {
                return RedirectToAction("Index", "Movies");
            }

            return View(model);
        }
    }
}
