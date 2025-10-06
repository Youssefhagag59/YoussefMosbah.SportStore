using SportsStore.Infrastructure;
using SportsStore.Interfaces;
using SportsStore.Models;

namespace SportsStore.Services
{ 
    public class CartService : ICartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartService(IHttpContextAccessor accessor)
        {
            _httpContextAccessor = accessor;
        }

        public Cart GetCart()
        {
            var cart = _httpContextAccessor?.HttpContext?.Session.GetJson<Cart>("Cart");

             return cart ?? new Cart();
        }

        public void AddToCart (Product product , int quantity )
        {
            var cart = GetCart();
            cart.AddItem(product, quantity);
            SaveCart(cart);
        }

        public void RemoveFromCart(Product product)
        {
            var cart = GetCart();
            cart.RemoveLine(product);
            SaveCart(cart);
        }

        public void ClearCart()
        {
            var cart= GetCart();
            cart.Clear();
            SaveCart(cart);
        }

        public void SaveCart(Cart cart)
        {
            _httpContextAccessor?.HttpContext?.Session.SetJson("Cart", cart);

        }
    }
}
