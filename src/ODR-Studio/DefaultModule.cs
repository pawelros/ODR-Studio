using System.Linq;
using log4net;
using Nancy;
using ODRStudio.ViewModels;
using ODR_Studio.WebApi.Client;
using System.IO;

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

                System.Collections.Generic.Dictionary<string, string> result = apiClient.RunDefault();

                return View["result.html", new Result { StandardOutput = result["standard_output"], StandardError = result["standard_error"] }];
            };

            Get["/download_eti"] = x =>
            {
                log.Debug("GET [/download_eti] route hit received");

                var response = 
                    new Response();

                response.Headers.Add("Content-Disposition", "attachment; filename=output.eti");
                response.ContentType = "text/plain";
                response.Contents = stream =>
                {
                    using (var writer = new StreamWriter(stream))
                    {
                        var raw = File.ReadAllText("/home/pr/open_digital_radio/ODR-DabMux/myfirst.eti");
                        writer.Write(raw);
                    }
                };

                return response;
            };
        }
    }
}

