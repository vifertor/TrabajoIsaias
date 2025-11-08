using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDExample.Domain.Models;
using DDDExample.Domain.Repositories;

using MongoDB.Driver;

namespace DDDExample.Infrastructure.Repositories
{
      public class MemoryMetricsRepository : IMemoryMetricsRepository
    {
        private readonly IMongoCollection<MemoryMetric> _collection;

        public MemoryMetricsRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<MemoryMetric>("MemoryMetrics");
        }

        public async Task AddAsync(MemoryMetric metric)
        {
            await _collection.InsertOneAsync(metric);
        }

        public async Task<IEnumerable<MemoryMetric>> GetRecentMetricsAsync(int limit = 100)
        {
            return await _collection
                .Find(_ => true)
                .SortByDescending(x => x.Timestamp)
                .Limit(limit)
                .ToListAsync();
        }
        public async Task<List<MemoryMetric>> GetAllAsync()
{
    return await _collection.Find(_ => true).ToListAsync();
}
    }
}