using SportsStore.Models;

namespace SportsStore.Interfaces
{
    public interface IStoreRepository
    {
        void CreateProduct(Product p);
        IQueryable<Product> Products { get; }
        void SaveProduct(Product p);
        void DeleteProduct(Product p);


    }
}
