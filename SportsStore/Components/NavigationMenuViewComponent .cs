using Microsoft.AspNetCore.Mvc;
using SportsStore.Interfaces;
using SportsStore.Models.ViewModels;



namespace SportsStore.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
       private IStoreRepository ? _repository;

        public NavigationMenuViewComponent(IStoreRepository repo)
        {
            _repository = repo;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(_repository?.Products
            .Select(c => c.Category)
            .Distinct()
            .OrderBy(c => c));
        }
            
            

        


    }
}
