namespace OdrStudio.WebApi.Models.Player
{
    public interface IMotEncoder
    {
        void Invoke(string slideshowDirPath, string dlsFilePath);
    }
}