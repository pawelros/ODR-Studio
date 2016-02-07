using System;
using System.Linq;

namespace OdrStudio.WebApi.Models.Player.Vlc
{
    internal static class Extensions
    {
        public static IPlayerStatus AsPlayerStatus(this VlcResponse vlcResponse)
        {
            PlayerStatus result = new PlayerStatus();

            switch (vlcResponse.State)
            {
                case "playing":
                    result.IsPlaying = true;
                    result.IsPaused = false;
                    result.IsStopped = false;
                    break;

                case "paused":
                    result.IsPlaying = false;
                    result.IsPaused = true;
                    result.IsStopped = false;
                    break;

                case "stopped":
                    result.IsPlaying = false;
                    result.IsPaused = false;
                    result.IsStopped = true;
                    break;
            }

            result.IsOnline = true;

            var information = vlcResponse.Infos[0];

            if (information.category != null)
            {
                var category = information.category.FirstOrDefault(c => c.name == "meta");

                if (category != null && category.info != null)
                {
                    var info = category.info.FirstOrDefault(i => i.name == "filename");

                    if (info != null)
                    {
                        string filename = info.Value;

                        result.TrackName = filename;
                    }
                }
            }

            result.TrackTime = Int32.Parse(vlcResponse.Time);
            result.TrackLength = Int32.Parse(vlcResponse.Length);
            result.TrackPosition = Double.Parse(vlcResponse.Position);

            return result;
        }
    }
}