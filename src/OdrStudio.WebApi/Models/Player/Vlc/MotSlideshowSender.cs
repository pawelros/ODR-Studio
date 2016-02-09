using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.OptionsModel;

namespace OdrStudio.WebApi.Models.Player.Vlc
{
    public class MotSlideshowSender : IMotSlideshowSender
    {
        private readonly IMotEncoder motEncoder;
        private readonly string motSlideShowBaseUri;
        private readonly ILogger logger;

        public MotSlideshowSender(IMotEncoder motEncoder, IOptions<PlayerConfiguration> configuration, ILoggerFactory loggerFactory)
        {
            this.motEncoder = motEncoder;
            this.motSlideShowBaseUri = configuration.Value.MotSlideshowUri;
            this.logger = loggerFactory.CreateLogger("MotSlideshowSender");
        }

        public void Send(string slideShowDir)
        {
            this.logger.LogVerbose($"got slideShowDir: {slideShowDir}");

            string pattern = @".*artistalbum";
            Regex rgx = new Regex(pattern);

            var localVlcPath = rgx.Replace(slideShowDir, this.motSlideShowBaseUri);
            this.logger.LogVerbose($"localVlcPath: {localVlcPath}");

            var localVlcUri = new Uri(localVlcPath);
            this.logger.LogVerbose($"localVlcUri: {localVlcUri}");

            var fileInfo = new FileInfo(localVlcUri.LocalPath);
            var directoryInfo = fileInfo.Directory;

            FileInfo dlsFile = directoryInfo.EnumerateFiles("*.txt").FirstOrDefault();

            this.motEncoder.Invoke(directoryInfo.FullName, dlsFile.FullName);
        }
    }
}