using EShop.Domain.Identity;
using EShop.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace EShop.Web.Controllers
{
    public class ShoppingCartController : Controller
    {

        private readonly IShoppingCartService _shoppingCartService;
        private readonly SignInManager<EShopApplicationUser> signInManager;

        public ShoppingCartController(IShoppingCartService shoppingCartService, SignInManager<EShopApplicationUser> signInManager)
        {
            _shoppingCartService = shoppingCartService;
            this.signInManager = signInManager;
        }

        public IActionResult Index()
        {
            if (signInManager.IsSignedIn(User))
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                return View(this._shoppingCartService.getShoppingCartInfo(userId));
            }

            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public IActionResult DeleteFromShoppingCart(Guid id)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this._shoppingCartService.deleteMovieFromSoppingCart(userId, id);

            if (result)
            {
                return RedirectToAction("Index", "ShoppingCart");
            }
            else
            {
                return RedirectToAction("Index", "ShoppingCart");
            }
        }

        public IActionResult Order()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this._shoppingCartService.order(userId);

            //if (result)
            //{
            //    return RedirectToAction("OrderConfirmed", "ShoppingCart");
            //} else
            //{
            //    return RedirectToAction("Index", "ShoppingCart");
            //}

            
            return RedirectToAction("Index", "ShoppingCart");
            
            
        }

        //public IActionResult OrderConfirmed()
        //{           
        //   return View();
        //}

        public IActionResult PayOrder(string stripeEmail, string stripeToken)
        {
            return View();
        }
    }
}
