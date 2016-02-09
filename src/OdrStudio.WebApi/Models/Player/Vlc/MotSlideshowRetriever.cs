using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.OptionsModel;

namespace OdrStudio.WebApi.Models.Player.Vlc
{
    public class MotSlideShowRetriever : IMotSlideShowRetriever
    {
        private readonly string motSlideShowUri;
        private readonly ILogger logger;

        public MotSlideShowRetriever(IOptions<PlayerConfiguration> configuration, ILoggerFactory loggerFactory)
        {
            this.motSlideShowUri = configuration.Value.MotSlideshowUri;
            this.logger = loggerFactory.CreateLogger("MotSlideShowRetriever");
        }

        public string[] Retrieve(string originVlcPath)
        {
            this.logger.LogVerbose($"Retrieving mot slide show from originVlcPath: {originVlcPath}");
            var originVlcUri = new Uri(originVlcPath);

            string pattern = ".*artistalbum";
            Regex rgx = new Regex(pattern);

            var localVlcPath = rgx.Replace(originVlcPath, this.motSlideShowUri);
            this.logger.LogVerbose($"localVlcPath: {localVlcPath}");
            
            var localVlcUri = new Uri(localVlcPath);
            this.logger.LogVerbose($"localVlcUri: {localVlcUri}");

            var fileInfo = new FileInfo(localVlcUri.LocalPath);
            var directoryInfo = fileInfo.Directory;

            string[] imagePaths = (directoryInfo.EnumerateFiles().Select(
                f => new Uri("file://" + rgx.Replace(f.FullName, string.Empty)).AbsolutePath))
                .ToArray();

            return imagePaths;
        }
    }
}