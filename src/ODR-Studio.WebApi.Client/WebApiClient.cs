using System.Linq;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ODR_Studio.WebApi.Client
{
    public class WebApiClient
    {
        private readonly string apiUrl;
        private readonly WebClient webClient;

        public WebApiClient(string apiUrl)
        {
            this.apiUrl = apiUrl;
            this.webClient = new WebClient();
        }

        public string[] GetConfigs()
        {
            string urlToHit = apiUrl + "/configs";
            string json = this.webClient.DownloadString(urlToHit);

            string[] configs = JsonConvert.DeserializeObject<string[]>(json);

            return configs;
        }

        public Dictionary<string,string> RunDefault()
        {
            string urlToHit = apiUrl + "/run";
            string json = this.webClient.DownloadString(urlToHit);

            var consoleOutput = JsonConvert.DeserializeObject<Dictionary<string,string>>(json);

            return consoleOutput;
        }
    }
}
