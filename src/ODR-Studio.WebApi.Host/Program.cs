using System;
using System.Configuration;
using System.Reflection;
using log4net;
using Nancy.Hosting.Self;
using Odr_DabMux_Runner;
using Nancy;

namespace ODR_Studio.WebApi.Host
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void Main(string[] args)
        {
            log.Debug("Starting WebApi.Host...");

            try
            {
                StaticConfiguration.DisableErrorTraces = false;
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
                Console.ReadLine();
            }

            log.Debug("Exiting WebApi.Host...");
        }
    }
}
