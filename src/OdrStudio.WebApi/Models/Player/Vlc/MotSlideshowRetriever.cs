using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNet.Mvc;
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

        public string[] RetrieveUrls(string originVlcPath)
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
                f =>
                {
                    this.logger.LogVerbose($"processing file: {f.FullName}");
                    string newFullName = rgx.Replace(f.FullName, "/artistalbum");
                    this.logger.LogVerbose($"newFullName: {newFullName}");

                    var uri = new Uri("file:///player/motslideshow" + newFullName);
                    this.logger.LogVerbose($"uri: {uri}");
                    this.logger.LogVerbose($"uri.AbsolutePath: {uri.AbsolutePath}");
                    this.logger.LogVerbose($"uri.AbsoluteUri: {uri.AbsoluteUri}");

                    return uri.AbsolutePath.Remove(0, 1);
                }))
                .ToArray();

            return imagePaths;
        }

        public FileResult RetrieveImage(string relativePath)
        {
            this.logger.LogVerbose($"Retrieving image using relative path: {relativePath}");

            var uri = new Uri(new Uri(this.motSlideShowUri), relativePath);

            this.logger.LogVerbose($"Retrieving image using uri local path: {uri.LocalPath}");

            return new FileStreamResult(new FileStream(uri.LocalPath, FileMode.Open), "image/jpeg");
        }
    }
}