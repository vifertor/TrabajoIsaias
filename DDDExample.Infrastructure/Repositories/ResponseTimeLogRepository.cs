using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDExample.Domain.Models;
using DDDExample.Domain.Repositories;
using MongoDB.Driver;

namespace DDDExample.Infrastructure.Repositories
{
   public class ResponseTimeLogRepository : IResponseTimeLogRepository
    {
        private readonly IMongoCollection<ResponseTimeLog> _collection;

        public ResponseTimeLogRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<ResponseTimeLog>("ResponseTimeLogs");
        }

        public async Task AddAsync(ResponseTimeLog log)
        {
            await _collection.InsertOneAsync(log);
        }
    }
}