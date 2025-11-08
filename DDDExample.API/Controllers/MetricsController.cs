using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DDDExample.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
namespace DDDExample.API.Controllers
{
     [Route("api/metrics")]
    [ApiController]
    public class MetricsController : ControllerBase
    {
        private readonly IMemoryMetricsRepository _repo;

        public MetricsController(IMemoryMetricsRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("memory")]
        public async Task<IActionResult> GetMemoryMetrics()
        {
            return Ok(await _repo.GetRecentMetricsAsync());
        }
    }
}