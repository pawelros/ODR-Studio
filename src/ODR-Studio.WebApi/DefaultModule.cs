using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Nancy;

namespace ODR_Studio.WebApi
{
    public class DefaultModule : NancyModule
    {
        public DefaultModule(ILog log)
        {
            log.Debug("Initializing DefaultModule...");

            Get["/"] = x =>
            {
                log.Debug("GET [/] route hit received");
                return "Hello from API!";
            };
        }
    }
}
