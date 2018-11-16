using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DutchTreat.Data
{
    public class DutchRepository : IDutchRepository
    {
        private readonly DutchContext _context;
        private readonly ILogger<DutchRepository> _logger
            ;

        public DutchRepository(DutchContext context, ILogger<DutchRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        // gets list of all products
        public IEnumerable<Product> GetAllProducts()
        {     
            try
            {
                _logger.LogInformation("GetAllProducts was called");
                return _context.Products
                    .OrderBy(p => p.Title)
                    .ToList();
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to get all products: {e}");
                return null;
            }
            
        }


        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return _context.Products
                .Where(p => p.Category == category)
                .ToList();
        }

        public Order GetOrderById(int id)
        {
            return _context.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .Where(o => o.Id == id)
                .FirstOrDefault();
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

        public void AddEntity(object model)
        {
            _context.Add(model);
        }

        public IEnumerable<Order> GetAllOrders(bool includeItems = true)
        {
            if (includeItems)
            {
                return _context.Orders
                    .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                    .ToList();
            }
            else
            {
                return _context.Orders
                    .ToList();
            }
            
        }
    }
}