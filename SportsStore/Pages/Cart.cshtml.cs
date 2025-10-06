using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SportsStore.Infrastructure;
using SportsStore.Interfaces;
using SportsStore.Models;
using SportsStore.Services;

namespace SportsStore.Pages {

    public class CartModel : PageModel {
        private readonly IStoreRepository _repository; 
        private readonly ICartService _cartService;
         

        public CartModel(IStoreRepository repo ,ICartService service) {
            _repository = repo;
            _cartService = service;
        }

        public Cart? Cart => _cartService.GetCart();
        public string ReturnUrl { get; set; } = "/";

        public void OnGet(string returnUrl) {
            ReturnUrl = returnUrl ?? "/";
        }

        public IActionResult OnPostAddToCart(long productId, string returnUrl) {
            Product? product = _repository.Products
                .FirstOrDefault(p => p.ProductID == productId);
            if (product != null) {
                _cartService.AddToCart(product, 1);
            }
            return RedirectToPage(new { returnUrl = returnUrl }); 
        }
         
        public IActionResult OnPostRemoveCart(long productId, string returnUrl)
        {
            Product? product = _repository.Products
                .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                _cartService.RemoveFromCart(product);
            }
            return RedirectToPage(new { returnUrl = returnUrl });
        }

        public IActionResult OnPostClearCart( string returnUrl)
        {
            
                _cartService.ClearCart();
            return RedirectToPage(new { returnUrl = returnUrl });
        }
    }
}
