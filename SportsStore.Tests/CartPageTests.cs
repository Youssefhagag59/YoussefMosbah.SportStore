using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Moq;
using SportsStore.Interfaces;
using SportsStore.Models;
using SportsStore.Pages;
using System.Linq;
using System.Text;
using System.Text.Json;
using Xunit;

namespace SportsStore.Tests
{
    public class CartPageTests
    {
        [Fact]

        public void Can_Load_cart()
        {
            //Arrange - create a mock repository 
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };

            var mockRepo = new Mock<IStoreRepository>();
            mockRepo.Setup(p => p.Products).Returns((new Product[] {p1, p2})
                .AsQueryable<Product>());


            //Arrage - create a new cart
            Cart testCart = new Cart();

            testCart.AddItem(p1, 2);
            testCart.AddItem(p2, 1);


            //Arrange - create a mock cart service  
            var mockCartService = new Mock<ICartService>();
            mockCartService.Setup(c=>c.GetCart()).Returns(testCart);


            var cartModel = new CartModel (mockRepo.Object, mockCartService.Object); 


            //Action 
            cartModel.OnGet("myUrl");

            //Assert 
            Assert.Equal(2,cartModel?.Cart?.Lines.Count());
            Assert.Equal("myUrl", cartModel?.ReturnUrl);


        }

        [Fact]
        public void Can_Update_Cart()
        {
            //Arrange - create a mock repository 
            Product p1 = new Product { ProductID = 1, Name = "P1" };

            var mockRepo = new Mock<IStoreRepository>();
            mockRepo.Setup(p => p.Products).Returns((new Product[] {p1})
                .AsQueryable<Product>());


            //Arrage - create a new cart
            Cart ? testCart = new Cart();

           

            //Arrange - create a mock page context and session  
            var mockSession = new Mock<ISession>();
            mockSession.Setup(s => s.Set(It.IsAny<string>(), It.IsAny<byte[]>()))
                .Callback<string, byte[]>((key, value) =>
                {
                    testCart = JsonSerializer.Deserialize<Cart>(Encoding.UTF8
                        .GetString(value));

                });
            var mockContext = new Mock<HttpContext>();
            mockContext.Setup(c => c.Session).Returns(mockSession.Object);


            
            //Arrange - create a mock cart service 
            var mockCartService = new Mock<ICartService>();
            mockCartService.Setup(c => c.GetCart()).Returns(testCart);

            var cartModel = new CartModel(mockRepo.Object,mockCartService.Object);

            //Action 
            cartModel.OnPostAddToCart(1, "myUrl");

            // Assert - check cart updated
            mockCartService.Verify(s=>s.AddToCart(p1,1) , Times.Once());



        }
    }
}
