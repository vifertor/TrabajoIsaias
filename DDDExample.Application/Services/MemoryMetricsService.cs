using System.Diagnostics;
using DDDExample.Application.Settings;
using DDDExample.Domain.Models;
using DDDExample.Domain.Repositories;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

namespace DDDExample.Application.Services
{
    public class MemoryMetricsService : BackgroundService
    {
        private readonly ILogger<MemoryMetricsService> _logger;
        private readonly MemoryMetricsSettings _settings;
        private readonly IServiceProvider _serviceProvider;
        private readonly PerformanceCounter _cpuCounter;

        public MemoryMetricsService(
            ILogger<MemoryMetricsService> logger,
            IOptions<MemoryMetricsSettings> settings,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _settings = settings.Value;
            _serviceProvider = serviceProvider;
            _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var repository = scope.ServiceProvider.GetRequiredService<IMemoryMetricsRepository>();

                    var process = Process.GetCurrentProcess();
                    var memoryMB = process.WorkingSet64 / 1024.0 / 1024.0;
                    var gcMemory = GC.GetTotalMemory(false) / 1024.0 / 1024.0;
                    var cpuUsage = _cpuCounter.NextValue();

                    var status = memoryMB >= _settings.CriticalThresholdMB ? "Critical" :
                                 memoryMB >= _settings.WarningThresholdMB ? "Warning" : "Normal";

                    await repository.AddAsync(new MemoryMetric
                    {
                        ProcessMemoryMB = memoryMB,
                        GCMemoryMB = gcMemory,
                        CpuUsage = cpuUsage,
                        Status = status
                    });
                }

                await Task.Delay(_settings.CollectionIntervalSeconds * 1000, stoppingToken);
            }
        }
    }
}
