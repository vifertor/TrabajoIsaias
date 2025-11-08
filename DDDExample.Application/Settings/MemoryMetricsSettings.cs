using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDDExample.Application.Settings
{
    public class MemoryMetricsSettings
    {
          public int CollectionIntervalSeconds { get; set; }
        public double WarningThresholdMB { get; set; }
        public double CriticalThresholdMB { get; set; }
    }
}