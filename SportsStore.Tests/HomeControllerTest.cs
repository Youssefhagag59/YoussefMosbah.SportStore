using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Interfaces;
namespace SportsStore.Tests 
{
    public class  HomeControllerTest
    { 
        private (HomeController controller , Mock mock) CreateControllerWithMock()
        {
           var mockRepo = new Mock<IStoreRepository>(); 
            mockRepo.Setup(m => m.Products).Returns((new Product[]
            {
                new Product {ProductID = 1, Name = "P1", Category = "Cat1" },
                new Product {ProductID = 2, Name = "P2", Category = "Cat2" },
                new Product {ProductID = 3, Name = "P3", Category = "Cat1" },
                new Product {ProductID = 4, Name = "P4", Category = "Cat2" },
                new Product {ProductID = 5, Name = "P5", Category = "Cat3" }

            }).AsQueryable<Product>());

            var controller =  new HomeController(mockRepo.Object)
            {
                pageSize = 3,
            };

            return (controller, mockRepo);

        }

        [Fact] 
        public void Can_Filter_Products()
        {
            //Arrange
            var (controller,mock) = CreateControllerWithMock();

            //Act 
            ProductsListViewModel result = controller.Index("Cat2",1)?.ViewData.Model
                as ProductsListViewModel ?? new();

            var products = result.Products.ToArray();

            //Assert 
            Assert.Equal(2, products.Length);
            Assert.True(products[0].Name == "P2" && products[0].Category == "Cat2");
            Assert.True(products[1].Name == "P4" && products[1].Category == "Cat2");
        }




        [Fact]
        public void Can_Use_Repository()
        {
            //Arange 
            var (controller, mock) = CreateControllerWithMock();


            //Act 
            ProductsListViewModel result =
                controller.Index(null) ?.ViewData.Model as ProductsListViewModel ?? new();


            //Assert 
            Product[] productArray = result.Products.ToArray() ?? Array.Empty<Product>();

            Assert.True(productArray.Length == 3);
            Assert.Equal("P1", productArray[0].Name);
            Assert.Equal("P2", productArray[1].Name);
        }


        [Fact]

        public void Can_Paginate()
        {
            //Arrange 
            var (controller, mock) = CreateControllerWithMock();
            
            //Act  
            ProductsListViewModel result = controller.Index(null,2)
                ?.ViewData.Model as ProductsListViewModel ?? new();



            //Assert 
            Product[]? prodArray = result.Products.ToArray();

            Assert.True(prodArray.Length == 2);
            Assert.Equal("P4", prodArray[0].Name);
            Assert.Equal("P5", prodArray[1].Name);



        }

        [Fact]
        public void Can_Send_Pagination_View_Model()
        {
            //Arrange 
            var (controller, mock) = CreateControllerWithMock();


            //Act 
            ProductsListViewModel result = controller.Index(null, 2)?.ViewData.Model as ProductsListViewModel ?? new();


            //Assert    
            PagingInfo pagingInfo = result.PagingInfo;
            Assert.Equal(2,pagingInfo.CurrentPage);
            Assert.Equal(3,pagingInfo.ItemsPerPage);
            Assert.Equal(5,pagingInfo.TotalItems);
            Assert.Equal(2,pagingInfo.TotalPages);

        }

        [Fact]
        public void Generate_Category_Specific_Product_Count()
        {
            //Arrange
            var (controller, mock) = CreateControllerWithMock();

            Func<ViewResult , ProductsListViewModel> GetModel = result =>
            result?.ViewData?.Model as ProductsListViewModel ?? new();


            //Act 
            int? res1 = GetModel(controller.Index("Cat1"))?.PagingInfo.TotalItems;
            int? res2 = GetModel(controller.Index("Cat2"))?.PagingInfo.TotalItems;
            int? res3  = GetModel(controller.Index("Cat3"))?.PagingInfo.TotalItems;
            int? resAll= GetModel(controller.Index(null))?.PagingInfo.TotalItems;


            
            //Assert 
            Assert.Equal(2,res1);
            Assert.Equal(2,res2);
            Assert.Equal(1,res3);
            Assert.Equal(5,resAll);










        }
    }
}
