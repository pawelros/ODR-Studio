using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.OptionsModel;

namespace OdrStudio.WebApi.Models.Player.Vlc
{
    public class VlcClient : IPlayerClient
    {
        private readonly Uri uri;
        private readonly ILogger logger;
        private readonly string username;
        private readonly string password;
        
        private readonly IMotSlideShowRetriever motSlideShowRetriever;

        public VlcClient(IOptions<PlayerConfiguration> configuration, ILoggerFactory loggerFactory, IMotSlideShowRetriever motSlideShowRetriever)
        {
            this.logger = loggerFactory.CreateLogger("VlcClient");
            this.uri = new Uri(configuration.Value.Uri);
            this.username = configuration.Value.Username;
            this.password = configuration.Value.Password;
            this.motSlideShowRetriever = motSlideShowRetriever;

            this.logger.LogVerbose($"VlcClient created with config uri {this.uri}");
        }

        public async Task<IPlayerStatus> GetStatus()
        {
            var statusUri = new Uri(this.uri, "/requests/status.xml");
            this.logger.LogVerbose($"Sending GET {statusUri}");

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var byteArray = Encoding.ASCII.GetBytes($"{this.username}:{this.password}");
                    var authHash = Convert.ToBase64String(byteArray);

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHash);
                    var response = await httpClient.GetAsync(statusUri);

                    var stream = await response.Content.ReadAsStreamAsync();

                    XmlSerializer serializer = new XmlSerializer(typeof(VlcResponse));
                    var result = serializer.Deserialize(stream) as VlcResponse;

                    var dto = result.AsPlayerStatus(this.motSlideShowRetriever);

                    return dto;
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.ToString());
                return new PlayerStatus { isOnline = false };
            }
        }
    }
}