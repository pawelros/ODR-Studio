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
        private readonly string motSlideShowBaseUri;
        private readonly ILogger logger;

        public MotSlideshowSender(IOptions<PlayerConfiguration> configuration, ILoggerFactory loggerFactory)
        {
            this.motSlideShowBaseUri = configuration.Value.MotSlideshowUri;
            this.logger = loggerFactory.CreateLogger("MotSlideshowSender");
        }

        public void Send(string slideShowDir, string dls)
        {
            this.logger.LogVerbose($"got slideShowDir: {slideShowDir}, dls: {dls}");

            string pattern = @".*artistalbum";
            Regex rgx = new Regex(pattern);

            var localVlcPath = rgx.Replace(slideShowDir, this.motSlideShowBaseUri);
            this.logger.LogVerbose($"localVlcPath: {localVlcPath}");

            var localVlcUri = new Uri(localVlcPath);
            this.logger.LogVerbose($"localVlcUri: {localVlcUri}");

            var fileInfo = new FileInfo(localVlcUri.LocalPath);
            var directoryInfo = fileInfo.Directory;

            FileInfo dlsFile = directoryInfo.EnumerateFiles("*.txt").FirstOrDefault();
            FileInfo globalDlsFile = new FileInfo("/mnt/hgfs/artistalbum/TestDls/dls.txt");

            if (dlsFile == null)
            {
                File.Create(Path.Combine(directoryInfo.FullName, "dls.txt"));
            }

            if (globalDlsFile == null)
            {
                File.Create(globalDlsFile.FullName);
            }

            if (dls != null)
            {
                File.WriteAllText(dlsFile.FullName, dls);
                File.WriteAllText(globalDlsFile.FullName, dls);
            }

            string[] imagePaths = (directoryInfo.EnumerateFiles("*.jpg").Select(
               f =>
               {
                   this.logger.LogVerbose($"processing file: {f.FullName}");
                   string newFullName = rgx.Replace(f.FullName, "/artistalbum");
                   this.logger.LogVerbose($"newFullName: {newFullName}");

                   var uri = new Uri("file://" + newFullName);
                   this.logger.LogVerbose($"uri: {uri}");
                   this.logger.LogVerbose($"uri.AbsolutePath: {uri.AbsolutePath}");
                   this.logger.LogVerbose($"uri.AbsoluteUri: {uri.AbsoluteUri}");

                   return uri.AbsolutePath.Remove(0, 1);
               }))
               .ToArray();

            FileInfo[] files = imagePaths.Select(i => RetrieveImage(i)).ToArray();

            //delete all files from global slideshow dir
            var globalSlideshowDir = new DirectoryInfo("/mnt/hgfs/artistalbum/TestSlideShow/");
            globalSlideshowDir.EnumerateFiles().ToList().ForEach(f => f.Delete());

            //copy all .jpg files from artistalbum to global dir
            foreach (var file in files)
            {
                this.logger.LogVerbose($"copying file: {file.FullName} to {globalSlideshowDir.FullName}");
                file.CopyTo(globalSlideshowDir.FullName);
            }
        }

        public FileInfo RetrieveImage(string relativePath)
        {
            this.logger.LogVerbose($"Retrieving image using relative path: {relativePath}");

            var uri = new Uri(new Uri(this.motSlideShowBaseUri), relativePath);

            this.logger.LogVerbose($"Retrieving image using uri local path: {uri.LocalPath}");

            return new FileInfo(uri.LocalPath);
        }
    }
}