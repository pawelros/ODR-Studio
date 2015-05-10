using System.Linq;
using System.Net;
using Newtonsoft.Json;

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

        public string RunDefault()
        {
            string urlToHit = apiUrl + "/run";
            string json = this.webClient.DownloadString(urlToHit);

            string[] consoleOutput = JsonConvert.DeserializeObject<string[]>(json);

            return consoleOutput.First();
        }
    }
}
