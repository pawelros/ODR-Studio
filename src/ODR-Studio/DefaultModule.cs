using System;
using Nancy;
using log4net;

namespace ODRStudio
{
    public class DefaultModule : NancyModule
    {
        public DefaultModule(ILog log)
        {
            log.Debug("Initializing DefaultModule...");

            Get["/"] = x =>
            {
                log.Debug("GET [/] route hit received");
                return "Hello World!";
            };
        }
    }
}

