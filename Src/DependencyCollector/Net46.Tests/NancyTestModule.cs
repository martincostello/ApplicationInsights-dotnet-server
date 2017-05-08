using System.Globalization;
using Microsoft.ApplicationInsights.Common;

namespace Microsoft.ApplicationInsights.DependencyCollector
{
    using System;
    using Nancy;
    using System.Collections.Generic;

    public class NancyTestModule : NancyModule
    {
        public static string AppId = Guid.NewGuid().ToString();

        public NancyTestModule()
        {
            Get["/"] = _ => new Response
            {
                StatusCode = HttpStatusCode.OK,
                Headers = new Dictionary<string, string>
                {
                    {
                        RequestResponseHeaders.RequestContextHeader,
                        string.Format(CultureInfo.InvariantCulture, "{0}=cid-v1:{1}", RequestResponseHeaders.RequestContextCorrelationTargetKey, NancyTestModule.AppId)
                    }
                }
            };
        }
    }
}
