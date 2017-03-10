﻿namespace Microsoft.ApplicationInsights.Extensibility.Filtering
{
    internal enum TelemetryType
    {
        Request = 0,

        Dependency = 1,

        Exception = 2,

        Event = 3,

        Metric = 4,

        PerformanceCounter = 5
    }
}