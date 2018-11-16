using System.Collections.Generic;
using DutchTreat.Data.Entities;

namespace DutchTreat.Data
{
    // reason for this interface is for testing so we can use mock data etc
    public interface IDutchRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);

        IEnumerable<Order> GetAllOrders(bool includeItems);
        Order GetOrderById(int id);

        bool SaveAll();

        void AddEntity(object model); // using object so any entity type can be added to database
    }
}