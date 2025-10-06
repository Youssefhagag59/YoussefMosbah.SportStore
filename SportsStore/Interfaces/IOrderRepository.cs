using SportsStore.Models;

namespace SportsStore.Interfaces
{
    public interface IOrderRepository
    {
        IQueryable<Order> Orders { get; }
        void SaveOrder(Order order);
    }
}
