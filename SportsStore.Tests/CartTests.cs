using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Models;
using Xunit;

namespace SportsStore.Tests
{
     public class CartTests
    {

        private (Cart cart , Product p1 , Product p2) CreateCartWithProducts()
        {
            Product p1 = new Product() { ProductID = 1, Name = "P1" };
            Product p2 = new Product() { ProductID = 2, Name = "P2" };
            Cart cart = new Cart();
            return (cart , p1, p2);
        }

        [Fact]
        public void Can_Add_New_Lines()
        {
            //Arrange
            var(cart,p1,p2) = CreateCartWithProducts();

            //Act
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 1);

            CartLine[] results =cart.Lines.ToArray();

            //Assert 
            Assert.Equal(2, results.Length);
            Assert.Equal(1, results[0].Product.ProductID);
            Assert.Equal(2, results[1].Product.ProductID);

        }

        [Fact]
        public void Can_Add_Qunatity_For_Exisiting_Lines()
        {
            //Arrange
            var(cart, p1, p2) = CreateCartWithProducts();

            //Act
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 1);
            cart.AddItem(p1, 10);

            CartLine[] results = cart.Lines.ToArray();

            //Assert 
            Assert.Equal(11, results[0].Quantity);
            Assert.Equal(1,results[1].Quantity);    

         
        }

        [Fact]
        public void Can_Remove_Line()
        {
            //Arrange - create some test products 
            Product p1 = new Product() { ProductID = 1, Name = "P1" };
            Product p2 = new Product() { ProductID = 2, Name = "P2" };
            Product p3 = new Product() { ProductID = 3, Name = "P3" };

            //Arrange - create a new cart 
            Cart cart = new Cart();

            //Act
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 1);
            cart.AddItem(p1, 10);
            cart.AddItem(p3, 1);

            cart.RemoveLine(p1);

            //Assert
            Assert.Empty(cart.Lines.Where(c=>c.Product == p1));
            Assert.Equal(2, cart.Lines.Count());

        }

        [Fact]
        public void Calculate_Cart_Total()
        {
            // Arrange - create some test products
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50M };


            //Arrange - create a new cart 
            Cart cart = new Cart();

            //Act
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 1);
            cart.AddItem(p1, 8);

             decimal result = cart.ComputeTotalValue();

            //Assert 
            Assert.Equal(950, result);
        }

        [Fact]
        public void Can_Clear_Contents()
        {
            // Arrange - create some test products
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50M };


            //Arrange - create a new cart 
            Cart cart = new Cart();

            //Act
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 1);
            cart.AddItem(p1, 8);

            cart.Clear();

            //Assert 
            Assert.Empty(cart.Lines);
        }

        


    }
}
