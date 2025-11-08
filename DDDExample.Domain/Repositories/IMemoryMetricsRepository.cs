using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks;
using DDDExample.Domain.Models;
namespace DDDExample.Domain.Repositories
{
    public interface IMemoryMetricsRepository
    {
         Task AddAsync(MemoryMetric metric);
        Task<IEnumerable<MemoryMetric>> GetRecentMetricsAsync(int limit = 100);
 Task<List<MemoryMetric>> GetAllAsync();
    }
}