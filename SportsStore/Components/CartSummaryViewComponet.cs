using Microsoft.AspNetCore.Mvc;
using SportsStore.Interfaces;
using SportsStore.Models;

namespace SportsStore.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private readonly ICartService _cartService;

        public CartSummaryViewComponent (ICartService cartService)
        {
            _cartService = cartService;
        }

        public IViewComponentResult Invoke()
        {
            var cart = _cartService.GetCart();
            return View(cart);
        }


    }
}
