using Microsoft.AspNetCore.Mvc;
using SportsStore.Interfaces;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartService _cartService; 
        public OrderController (IOrderRepository orderRepository ,ICartService cartService)
        {
            _orderRepository = orderRepository;
            _cartService = cartService;
        }

        [HttpGet]
        public IActionResult Checkout() => View(new CheckoutViewModel());

        [HttpPost]
        public IActionResult Checkout(CheckoutViewModel vm)
        {
          var cart = _cartService.GetCart();
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty");
            }

             if (ModelState.IsValid)
            {
                var order = new Order
                {
                    Name = vm.Name,
                    Line1 = vm.Line1,
                    Line2 = vm.Line2,
                    Line3 = vm.Line3,
                    City = vm.City,
                    State = vm.State,
                    Zip = vm.Zip,
                    Country = vm.Country,
                    Phone = vm.Phone,
                    GiftWrap = vm.GiftWrap,
                    CartLines = cart.Lines.ToArray()

                };

                _orderRepository.SaveOrder(order);
                 _cartService.ClearCart();
                return RedirectToPage("/Completed", new { orderId = order.OrderID });
            }
             else 
            {
                return View();
            }
            
        }

        
       
    } 
}
