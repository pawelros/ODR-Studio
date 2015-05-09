using System;
using System.Configuration;
using log4net;
using Nancy.Hosting.Self;

namespace ODRStudio.Host
{
    class MainClass
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void Main(string[] args)
        {
            log.Debug("Starting Host...");

            try
            {
                string hostUrl = ConfigurationManager.AppSettings["HostUrl"];
                using (var host = new NancyHost(new Uri(hostUrl), new CustomNancyBootstrapper(log)))
                {
                    log.DebugFormat("Nancy started on url: {0}", hostUrl);
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
