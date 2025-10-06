using Microsoft.AspNetCore.Mvc;
using SportsStore.Interfaces;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStoreRepository _repository;

        public int pageSize = 3;

        public HomeController(IStoreRepository repo)
        {
            _repository = repo;
        }

        public ViewResult Index(string? category, int productPage = 1)
            => View(new ProductsListViewModel
            {
                Products = _repository.Products
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(p => p.ProductID)
                    .Skip((productPage - 1) * pageSize)
                    .Take(pageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = pageSize, 
                    TotalItems = _repository.Products
                        .Where(p => category == null || p.Category == category)
                        .Count()
                },

                CurrentCategory = category 
            });
    }
}
