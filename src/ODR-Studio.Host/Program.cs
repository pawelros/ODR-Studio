using System;
using log4net;

namespace ODRStudio
{
    class MainClass
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void Main(string[] args)
        {
            log.Debug("Starting application...");
            log.Info("Starting application...");
        }
    }
}
