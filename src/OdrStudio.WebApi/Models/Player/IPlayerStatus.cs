namespace OdrStudio.WebApi.Models.Player
{
    public interface IPlayerStatus
    {
        bool isOnline { get; set; }
        bool isPlaying { get; set; }
        bool isStopped { get; set; }
        bool isPaused { get; set; }
        string trackName { get; set; }
        int trackTime { get; set; }
        int trackLength { get; set; }
        double trackPosition { get; set; }
    }
}