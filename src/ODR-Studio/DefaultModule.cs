using System.Linq;
using log4net;
using Nancy;
using ODRStudio.ViewModels;
using ODR_Studio.WebApi.Client;

namespace ODRStudio
{
    public class DefaultModule : NancyModule
    {
        private readonly WebApiClient apiClient;

        public DefaultModule(ILog log, WebApiClient apiClient)
        {
            this.apiClient = apiClient;
            log.Debug("Initializing DefaultModule...");

            Get["/"] = x =>
            {
                log.Debug("GET [/] route hit received");

                var viewModel = new Index { Configs = apiClient.GetConfigs() };
                viewModel.CurrentConfig = viewModel.Configs.First();

                return View["index.html", viewModel];
            };

            Get["/run"] = x =>
            {
                log.Debug("GET [/run] route hit received");

                string result = apiClient.RunDefault();

                return View["result.html", new Result { Log = result }];
            };
        }
    }
}

