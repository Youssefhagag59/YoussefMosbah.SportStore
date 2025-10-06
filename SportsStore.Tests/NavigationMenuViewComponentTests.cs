using Moq;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Components;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using SportsStore.Interfaces;

namespace SportsStore.Tests
{
   public class NavigationMenuViewComponentTests 
    {
        [Fact]
        public void Can_Select_Categories() {
            //Arrange
            var mock = new Mock<IStoreRepository>();
            mock.Setup(p => p.Products).Returns((new Product[]
            {
                new Product { ProductID = 1 , Name = "P1" , Category = "Apples"},
                new Product { ProductID = 1 , Name = "P2" , Category = "Apples"},
                new Product { ProductID = 1 , Name = "P3" , Category = "Plums"},
                new Product { ProductID = 1 , Name = "P4" , Category = "Oranges"},
            }).AsQueryable<Product>());

        NavigationMenuViewComponent target = new NavigationMenuViewComponent(mock.Object);


            //Act = get the set of categories 
            string [] results = ((IEnumerable<string>?)(target.Invoke()
                   as ViewViewComponentResult)?.ViewData?.Model ?? Enumerable.Empty<string>()).ToArray();


            //Assert 
            Assert.True(Enumerable.SequenceEqual(new string[] { "Apples", "Oranges", "Plums" }, results));





        }

        [Fact] 

        public void Indicates_Selected_Category()
        {
            //Arrange 
            var mock = new Mock<IStoreRepository>();
            mock.Setup(p => p.Products).Returns((new Product[]
            {
                new Product {ProductID = 1, Name = "P1", Category = "Cat1" },
                new Product {ProductID = 2, Name = "P2", Category = "Cat2" },
            }).AsQueryable<Product>());

            var viewComponet = new NavigationMenuViewComponent(mock.Object);

            //Build fake route data
            var routeData = new RouteData();
            routeData.Values["category"] = "Cat1";


            //Build fake ViewComponentContext
            var viewComponentContext = new ViewComponentContext
            {
                ViewContext = new ViewContext
                {
                    RouteData = routeData,
                    HttpContext = new DefaultHttpContext()
                }
            };

            viewComponet.ViewComponentContext = viewComponentContext;


            //Act 
            var result = viewComponet.Invoke() as ViewViewComponentResult;



            //Assert 
            Assert.Equal("Cat1", result?.ViewData["SelectedCategory"]);




             

        }


    }
}
