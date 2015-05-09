using log4net;
using Nancy;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Odr_DabMux_Runner;

namespace ODR_Studio.WebApi
{
    public class DefaultModule : NancyModule
    {

        public DefaultModule(ILog log)
        {
            log.Debug("Initializing DefaultModule...");

            Get["/configs"] = x =>
            {
                log.Debug("GET [/] route hit received");

                var configs = new[] {DabMuxConfig.OutputToFileConfig};
                var json = JsonConvert.SerializeObject(configs);

                return json;
            };
        }
    }
}
