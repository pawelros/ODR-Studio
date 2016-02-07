namespace OdrStudio.WebApi.Models.Player
{
    public class PlayerStatus : IPlayerStatus
    {
        public bool IsOnline { get; set; }
        public bool IsPlaying { get; set; }
        public bool IsStopped { get; set; }
        public bool IsPaused { get; set; }
        public string TrackName { get; set; }
        public int TrackTime { get; set; }
        public int TrackLength { get; set; }
        public double TrackPosition { get; set; }
    }
}