using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.OptionsModel;

namespace OdrStudio.WebApi.Models.Player.Vlc
{
    public class DlsRetriever : IDlsRetriever
    {
        private readonly string motSlideShowUri;
        private readonly ILogger logger;

        public DlsRetriever(IOptions<PlayerConfiguration> configuration, ILoggerFactory loggerFactory)
        {
            this.motSlideShowUri = configuration.Value.MotSlideshowUri;
            this.logger = loggerFactory.CreateLogger("DlsRetriever");
        }

        public string RetrieveDls(string originVlcPath)
        {
            this.logger.LogVerbose($"Retrieving dls from originVlcPath: {originVlcPath}");
            var originVlcUri = new Uri(originVlcPath);

            string pattern = ".*artistalbum";
            Regex rgx = new Regex(pattern);

            var localVlcPath = rgx.Replace(originVlcPath, this.motSlideShowUri);
            this.logger.LogVerbose($"localVlcPath: {localVlcPath}");

            var localVlcUri = new Uri(localVlcPath);
            this.logger.LogVerbose($"localVlcUri: {localVlcUri}");

            var fileInfo = new FileInfo(localVlcUri.LocalPath);
            var directoryInfo = fileInfo.Directory;

            FileInfo dlsFile = directoryInfo.EnumerateFiles("*.txt").FirstOrDefault();

            if (dlsFile != null)
            {
                this.logger.LogVerbose($"Reading dls from file: {dlsFile.FullName}");
                string dls = File.ReadAllText(dlsFile.FullName);
                this.logger.LogVerbose($"DLS: {dls}");

                return dls;
            }

            return null;
        }
    }
}