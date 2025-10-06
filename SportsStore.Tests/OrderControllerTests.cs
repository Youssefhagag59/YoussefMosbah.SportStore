using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Interfaces;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Tests
{
   public class OrderControllerTests
    {
        [Fact]
        public void Cannot_Checkout_Empty_Cart()
        {
            //Arrage - mocks 
            var mockRepo = new Mock<IOrderRepository>();
            var mockCartService = new Mock<ICartService>();
            mockCartService.Setup(c => c.GetCart()).Returns(new Cart());

            //Arrange - controller 
            var orderController = new OrderController(mockRepo.Object,mockCartService.Object);

            //Action
            var resutl = orderController.Checkout(new CheckoutViewModel());

            //Assert 
            Assert.False(orderController.ModelState.IsValid);
            Assert.True(orderController.ModelState.Count > 0);


        } 

        [Fact]
        public void Cannot_Checkout_Invalid_Details()
        {
            //Arrange 
            var mockRepo= new Mock<IOrderRepository>();
            var mockCartService = new Mock<ICartService>();

            var cart = new Cart();
            cart.AddItem(new Product(), 1);
            mockCartService.Setup(c=>c.GetCart()).Returns(cart);

            var ordrController = new OrderController(mockRepo.Object,mockCartService.Object);

            ordrController.ModelState.AddModelError("error", "Ivalid shipping details");



            //Action
            var result = ordrController.Checkout(new CheckoutViewModel()) as ViewResult;



            //Assert - make sure SaveOrder  was never called because data is invalid 
            mockRepo.Verify(r => r.SaveOrder(It.IsAny<Order>()) ,Times.Never);

            //Assert 
            Assert.True(string.IsNullOrEmpty(result?.ViewName));
            Assert.False(result?.ViewData.ModelState.IsValid);



        }

        [Fact]
        public void Can_Checkout_And_Submit_Order()
        {
            //Arrange 
            var mockRepo = new Mock<IOrderRepository>();
            var mockCartService= new Mock<ICartService>();

            var cart = new Cart();
            cart.AddItem(new Product(), 1);

            mockCartService.Setup(c=>c.GetCart()).Returns(cart);

            var orderController = new OrderController(mockRepo.Object,mockCartService.Object);

            //Action 
            var result = orderController.Checkout(new CheckoutViewModel()) as RedirectToPageResult;


            //Assert - make sure SavOrder is called once only
            mockRepo.Verify(r=>r.SaveOrder(It.IsAny<Order>()),Times.Once);

            //Assert
            Assert.Equal("/Completed" , result?.PageName);

        }
    }
}
