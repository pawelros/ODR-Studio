using System.Dynamic;
using System.IO;

namespace Odr_DabMux_Runner
{
    public static class DabMuxConfig
    {
        private static string outputToFileConfig;

        public static string OutputToFileConfig
        {
            get
            {
                if (string.IsNullOrWhiteSpace(outputToFileConfig))
                {
                    outputToFileConfig = File.ReadAllText(@"Configs/OutputToFile.mux");
                }

                return outputToFileConfig;
            }
        } 
    }
}
