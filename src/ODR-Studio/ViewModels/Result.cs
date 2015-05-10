using System.Reflection;

namespace ODRStudio.ViewModels
{
    public class Result
    {
        public string Version { get; } = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public string StandardOutput { get; set; }

        public string StandardError { get; set; }
    }
}