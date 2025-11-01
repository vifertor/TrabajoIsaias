using MongoDB.Driver;
using DDDExample.Domain.Entities;
using DDDExample.Domain.Repositories;

namespace DDDExample.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _collection;

        public ProductRepository(IMongoDatabase db)
        {
            _collection = db.GetCollection<Product>("Products");
        }

        public async Task<IEnumerable<Product>> GetAllAsync() =>
            await _collection.Find(_ => true).ToListAsync();

        public async Task<Product?> GetByIdAsync(Guid id) =>
            await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();

        public async Task AddAsync(Product product) =>
            await _collection.InsertOneAsync(product);

        public async Task UpdateAsync(Product product) =>
            await _collection.ReplaceOneAsync(x => x.Id == product.Id, product);

        public async Task DeleteAsync(Guid id) =>
            await _collection.DeleteOneAsync(x => x.Id == id);
    }
}
