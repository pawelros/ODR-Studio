namespace OdrStudio.WebApi.Models.Player
{
    public interface IPlayerStatus
    {
        bool IsOnline { get; set; }
        bool IsPlaying { get; set; }
        bool IsStopped { get; set; }
        bool IsPaused { get; set; }
        string TrackName { get; set; }
        int TrackTime { get; set; }
        int TrackLength { get; set; }
        double TrackPosition { get; set; }
    }
}