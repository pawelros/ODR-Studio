using Microsoft.AspNet.Mvc;

namespace OdrStudio.WebApi.Models.Player
{
    public interface IMotSlideShowRetriever
    {
        string[] RetrieveUrls(string basePath);

        FileResult RetrieveImage(string relativePath);
    }
}