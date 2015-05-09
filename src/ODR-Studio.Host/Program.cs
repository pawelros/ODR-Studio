using System;
using log4net;
using Nancy.Hosting.Self;
using ODRStudio.Host;

namespace ODRStudio
{
    class MainClass
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void Main(string[] args)
        {
            log.Debug("Starting application...");

            try
            {
                using (var host = new NancyHost(new Uri(Settings.HostUrl)))
                {
                    log.DebugFormat("Nancy started on url: {0}", Settings.HostUrl);
                    host.Start();
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
            }
        }
    }
}
