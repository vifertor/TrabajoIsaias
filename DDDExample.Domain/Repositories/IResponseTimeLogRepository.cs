using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDExample.Domain.Models;

namespace DDDExample.Domain.Repositories
{
    public interface IResponseTimeLogRepository
    {
        Task AddAsync(ResponseTimeLog log);
    }
}