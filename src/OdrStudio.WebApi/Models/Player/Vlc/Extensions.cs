using System;
using System.Linq;

namespace OdrStudio.WebApi.Models.Player.Vlc
{
    internal static class Extensions
    {
        public static IPlayerStatus AsPlayerStatus(this VlcResponse vlcResponse, IMotSlideShowRetriever motSlideShowRetriever, IDlsRetriever dlsRetriever)
        {
            PlayerStatus result = new PlayerStatus();

            switch (vlcResponse.State)
            {
                case "playing":
                    result.isPlaying = true;
                    result.isPaused = false;
                    result.isStopped = false;
                    break;

                case "paused":
                    result.isPlaying = false;
                    result.isPaused = true;
                    result.isStopped = false;
                    break;

                case "stopped":
                    result.isPlaying = false;
                    result.isPaused = false;
                    result.isStopped = true;
                    break;
            }

            result.isOnline = true;

            var information = vlcResponse.Infos[0];

            if (information.category != null)
            {
                var category = information.category.FirstOrDefault(c => c.name == "meta");

                if (category != null && category.info != null)
                {
                    var fileNameInfo = category.info.FirstOrDefault(i => i.name == "filename");
                    var artistInfo = category.info.FirstOrDefault(i => i.name == "ARTIST");
                    var titleInfo = category.info.FirstOrDefault(i => i.name == "TITLE");
                    var artworkUrl = category.info.FirstOrDefault(i => i.name == "artwork_url");

                    if (artistInfo != null && titleInfo != null)
                    {
                        string artist = artistInfo.Value;
                        string title = titleInfo.Value;

                        result.trackName = $"{artist} - {title}";
                    }
                    else if (fileNameInfo != null)
                    {
                        string filename = fileNameInfo.Value;

                        result.trackName = filename;
                    }

                    if (artworkUrl != null)
                    {
                        result.motSlideShowUrls = motSlideShowRetriever.RetrieveUrls(artworkUrl.Value);
                        result.dls = dlsRetriever.RetrieveDls(artworkUrl.Value);
                    }
                }
            }

            result.trackTime = Int32.Parse(vlcResponse.Time);
            result.trackLength = Int32.Parse(vlcResponse.Length);
            result.trackPosition = Double.Parse(vlcResponse.Position);

            return result;
        }
    }
}