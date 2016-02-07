namespace OdrStudio.WebApi.Models.Player
{
    public class PlayerStatus : IPlayerStatus
    {
        public bool isOnline { get; set; }
        public bool isPlaying { get; set; }
        public bool isStopped { get; set; }
        public bool isPaused { get; set; }
        public string trackName { get; set; }
        public int trackTime { get; set; }
        public int trackLength { get; set; }
        public double trackPosition { get; set; }
    }
}