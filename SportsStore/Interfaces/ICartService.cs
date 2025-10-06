using SportsStore.Models;

namespace SportsStore.Interfaces
{
    public interface ICartService
    {
        Cart GetCart();
        void AddToCart(Product product, int quantity);
        void RemoveFromCart(Product product);
        void ClearCart();
        void SaveCart(Cart cart);

    }
}
