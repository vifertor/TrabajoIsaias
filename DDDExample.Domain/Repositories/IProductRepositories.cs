using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDExample.Domain.Entities;

namespace DDDExample.Domain.Repositories
{
 public interface IProductRepository : IRepository<Product>
  {
      Task<IEnumerable<Product>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice);
      Task<IEnumerable<Product>> GetProductsInStockAsync();
  }
}