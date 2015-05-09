using System.IO;

namespace Odr_DabMux_Runner
{
    public static class DabMuxConfig
    {
        private static readonly object lock_object = new object();
        private static string currentConfig;

        public static string CurrentConfig
        {
            get
            {
                lock (lock_object)
                {
                    return currentConfig;
                }
            }
            set
            {
                lock (lock_object)
                {
                    currentConfig = value;
                }
            }
        }

        public static string OutputToFileConfig => File.ReadAllText(@"Configs/OutputToFile.mux");
    }
}
